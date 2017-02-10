using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Tiger.CodeGeneration;
using Tiger.Parsing;
using Tiger.Semantics;
using Tiger.AST;
using System.Linq.Expressions;

namespace Tiger
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Wrong parameter number.");
                Environment.ExitCode = (int)ErrorCodes.WrongParameters;
                PrintHelp();
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.Error.WriteLine("Input file path is not valid, does not exist or user has no sufficient permission to read it.");
                Environment.ExitCode = (int)ErrorCodes.FileError;
                return;
            }
            //try
            {
                ProcessFile(args[0], Path.ChangeExtension(args[0], "exe"));
            }
            /*catch (Exception exc)
            {
                Console.Error.WriteLine("Unexpected error: {0}", exc.Message);
                Environment.ExitCode = (int)ErrorCodes.UnexpectedError;
            }*/
        }

        static void PrintHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("sl.exe [<input_file>]");
            Console.WriteLine("<input_file>:\tPath to file written in SimpleLanguage to be compiled");
            Console.WriteLine("Without parameters programm will print this help.");
        }

        static void ProcessFile(string inputPath, string outputPath)
        {
            Console.WriteLine("Tiger Compiler version 1.0\nCopyright (C) 2017 Eddy Almuiña\n");

            //Console.WriteLine("Building: {0}", Path.GetFullPath(inputPath));
            Node root = ParseInput(inputPath);
            if (root == null)
            {
                Environment.ExitCode = (int)ErrorCodes.SyntaxError;
                return;
            }
            //Console.WriteLine("No syntax error found");

            if (!CheckSemantics(root))
            {
                Environment.ExitCode = (int)ErrorCodes.SemanticError;
                return;
            }
            //Console.WriteLine("No semantic error found.");

            GenerateCode(root, outputPath);
            //Console.WriteLine("Success");
        }

        static Node ParseInput(string inputPath)
        {
            //try
            {
                var input = new AntlrFileStream(inputPath);
                var lexer = new TigerLexer(input);

                var tokens = new CommonTokenStream(lexer);
                var parser = new TigerParser(tokens);
                parser.RemoveErrorListeners();
                parser.AddErrorListener(new ErrorListener());

                IParseTree tree = parser.compileUnit();
                var astBuilder = new ASTBuilder();
                var ast = astBuilder.Visit(tree);
                return ast as Node;
            }
            //catch (Exception)
            //{
            //    return null;
            //}
        }

        static bool CheckSemantics(Node root)
        {
            List<SemanticError> errors = new List<SemanticError>();
            root.CheckSemantics(new Scope(), errors);
            if (errors.Count == 0)
                return true;
            foreach (var error in errors)
                Console.Error.WriteLine("({1}, {2}): {0}.", error.Message, error.Node.Line, error.Node.Column);
            return false;
        }

        static void GenerateCode(Node root, string outputPath)
        {
            var generator = new CodeGenerator(outputPath);

            string name = Path.GetFileNameWithoutExtension(outputPath);
            string filename = Path.GetFileName(outputPath);
            AssemblyName assemblyName = new AssemblyName(name);
            generator.Assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave, Path.GetDirectoryName(outputPath));
            generator.Module = generator.Assembly.DefineDynamicModule(name, filename);

            SymbolTable symbols = BuildStdl(generator.Module);

            generator.Type = generator.Module.DefineType("Program");
            MethodBuilder mainMethod = generator.Type.DefineMethod("Main", MethodAttributes.Static, typeof(void), System.Type.EmptyTypes);
            generator.Assembly.SetEntryPoint(mainMethod);
            generator.Generator = mainMethod.GetILGenerator();

            root.Generate(generator, symbols);

            generator.Type.CreateType();
            generator.Module.CreateGlobalFunctions();
            generator.Assembly.Save(filename);
        }

        static SymbolTable BuildStdl(ModuleBuilder builder)
        {
            var symbols = new SymbolTable();
            var stdl = builder.DefineType("StandardLibrary");

            //TODO ord, chr

            dynamic functions = new Expression[]
            {
                (Expression<Action<int>>)(s => Console.Write(s)), //printi
                (Expression<Action<string>>)(s => Console.Write(s)), //print
                (Expression<Func<string>>)(() => Console.ReadLine()), //getline
                (Expression<Action<string>>)(s => Console.WriteLine(s)), //printline
                (Expression<Action<int>>)(n => Console.WriteLine(n)), //printiline
                (Expression<Func<string, int>>)(s => s.Length), //size
                (Expression<Func<string, int, int, string>>)((s, f, n) => s.Substring(f, n)), //substring
                (Expression<Func<string, string, string>>)((s1, s2) => s1+s2), //concat
                (Expression<Func<int, int>>)(i => i == 0 ? 1 : 0), //not
                (Expression<Action<int>>)(i => Environment.Exit(i)), //exit
            };

            var names = new string[]
            {
                "printi", "print", "getline", "printline", "printiline", "size", "substring", "concat", "not", "exit"
            };

            for (int i = 0; i < functions.Length; i++)
            {
                var func = functions[i];
                var name = names[i];

                MethodBuilder method = stdl.DefineMethod(name, MethodAttributes.Static);
                func.CompileToMethod(method);
                symbols.Functions[name] = method;
            };

            stdl.CreateType();
            return symbols;
        }
    }
}
