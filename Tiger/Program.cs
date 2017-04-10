using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Tiger.CodeGeneration;
using Tiger.Parsing;
using Tiger.Semantics;
using Tiger.AST;
using System.Linq;

namespace Tiger
{
    class Program
    {
        static readonly int ErrorCode = 1;

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("(0,0): Wrong parameter number.");
                Environment.ExitCode = ErrorCode;
                PrintHelp();
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.Error.WriteLine("(0,0): Input file path is not valid, does not exist or user has no sufficient permission to read it.");
                Environment.ExitCode = ErrorCode;
                return;
            }

            ProcessFile(args[0], Path.ChangeExtension(args[0], "exe"));
            Console.WriteLine();
        }

        static void PrintHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("tiger.exe [<input_file>]");
            Console.WriteLine("<input_file>:\tPath to file written in Tiger to be compiled");
            Console.WriteLine("Without parameters programm will print this help.");
        }

        static void ProcessFile(string inputPath, string outputPath)
        {
            Console.WriteLine("Tiger Compiler version 1.0\nCopyright (C) 2017 Eddy Almuiña");

            Node root = ParseInput(inputPath);
            var scope = new Scope();

            if (root == null || !CheckSemantics(root, scope))
            {
                Environment.ExitCode = ErrorCode;
                return;
            }

            GenerateCode(root, outputPath, scope);
        }

        static Node ParseInput(string inputPath)
        {
            try
            {
                var input = new AntlrFileStream(inputPath);
                var lexer = new TigerLexer(input);

                var errors = new List<string>();
                lexer.RemoveErrorListeners();
                lexer.AddErrorListener(new LexerErrorListener(errors));

                var tokens = new CommonTokenStream(lexer);
                var parser = new TigerParser(tokens);

                parser.RemoveErrorListeners();
                parser.AddErrorListener(new ParserErrorListener(errors));

                IParseTree tree = parser.compileUnit();

                if (errors.Any())
                {
                    Console.WriteLine();
                    foreach (var error in errors)
                        Console.WriteLine(error);
                    return null;
                }

                var astBuilder = new ASTBuilder();
                Node ast = astBuilder.Visit(tree);
                return ast;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return null;
            }
        }

        static bool CheckSemantics(Node root, Scope scope)
        {
            List<SemanticError> errors = new List<SemanticError>();
            root.CheckSemantics(scope, errors);
            if (errors.Count == 0)
                return true;
            Console.WriteLine();
            foreach (var error in errors)
                Console.WriteLine($"({error.Node.Line},{error.Node.Column}): {error.Message}.");
            return false;
        }

        static void GenerateCode(Node root, string outputPath, Scope scope)
        {
            var generator = new CodeGenerator();

            string name = Path.GetFileNameWithoutExtension(outputPath);
            string filename = Path.GetFileName(outputPath);
            string directory = Path.GetDirectoryName(outputPath);

            directory = directory == "" ? "./" : directory;

            AssemblyName assemblyName = new AssemblyName(name);
            generator.Assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave, directory);
            generator.Module = generator.Assembly.DefineDynamicModule(name, filename);

            StandardLibrary.Build(generator, scope);

            generator.Type = generator.Module.DefineType("Program");
            MethodBuilder mainMethod = generator.Type.DefineMethod("Main", MethodAttributes.Static, typeof(void), Type.EmptyTypes);
            generator.Assembly.SetEntryPoint(mainMethod);
            generator.Method = mainMethod;
            generator.Generator = mainMethod.GetILGenerator();

            root.Generate(generator);

            generator.Type.CreateType();
            generator.Module.CreateGlobalFunctions();
            generator.Assembly.Save(filename);
        }
    }
}
