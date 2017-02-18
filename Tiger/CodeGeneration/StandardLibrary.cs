using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq.Expressions;
using Tiger.Semantics;

namespace Tiger.CodeGeneration
{
    static class StandardLibrary
    {
        public static void Build(CodeGenerator generator, Scope scope)
        {
            ModuleBuilder builder = generator.Module;
            var funcs = typeof(StandardLibrary).GetMethods().Where(m => scope.UsedStdlFunctions.Contains(m.Name.ToLower()));
            TypeBuilder stdl = null;

            foreach (var f in funcs)
            {
                var args = new List<object>();
                if (f.GetParameters().Length > 0)
                {
                    if (stdl == null)
                        stdl = builder.DefineType("StandardLibrary");
                    args.Add(stdl);
                }

                var method = (MethodInfo)f.Invoke(null, args.ToArray());
                generator.Functions[f.Name.ToLower()] = method;
            }

            if (stdl != null)
                stdl.CreateType();
        }

        public static MethodInfo Print()
        {
            return typeof(Console).GetMethod("Write", new[] { typeof(string) });
        }

        public static MethodInfo Printi()
        {
            return typeof(Console).GetMethod("Write", new[] { typeof(int) });
        }

        public static MethodInfo GetLine()
        {
            return typeof(Console).GetMethod("ReadLine");
        }

        public static MethodInfo PrintLine()
        {
            return typeof(Console).GetMethod("WriteLine", new[] { typeof(string) });
        }

        public static MethodInfo PrintiLine()
        {
            return typeof(Console).GetMethod("WriteLine", new[] { typeof(int) });
        }

        public static MethodInfo Chr(TypeBuilder stdl)
        {
            MethodBuilder method = stdl.DefineMethod("chr", MethodAttributes.Static, typeof(string), new[] { typeof(int) });
            var il = method.GetILGenerator();

            Label error = il.DefineLabel();
            Label end = il.DefineLabel();

            il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Blt, error);

            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Ldc_I4, 126);
            il.Emit(OpCodes.Bgt, error);

            MethodInfo toChar = typeof(Convert).GetMethod("ToChar", new[] { typeof(int) });
            il.Emit(OpCodes.Call, toChar);
            MethodInfo toString = typeof(Convert).GetMethod("ToString", new[] { typeof(char) });
            il.Emit(OpCodes.Call, toString);
            il.Emit(OpCodes.Br, end);

            il.MarkLabel(error);
            il.ThrowException(typeof(ArgumentException));
            il.MarkLabel(end);
            il.Emit(OpCodes.Ret);

            return method;
        }

        public static MethodInfo Ord(TypeBuilder stdl)
        {
            MethodBuilder method = stdl.DefineMethod("ord", MethodAttributes.Static);
            Expression<Func<string, int>> ord = (s) => (s == null || s.Length == 0) ? -1 : (byte)s[0];
            ord.CompileToMethod(method);
            return method;
        }

        public static MethodInfo Size()
        {
            return typeof(string).GetProperty("Length").GetMethod;
        }

        public static MethodInfo Substring()
        {
            return typeof(string).GetMethod("Substring", new[] { typeof(int), typeof(int) });
        }

        public static MethodInfo Concat()
        {
            return typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });
        }

        public static MethodInfo Not(TypeBuilder stdl)
        {
            MethodBuilder method = stdl.DefineMethod("not", MethodAttributes.Static);
            Expression<Func<int, int>> not = (i) => i == 0 ? 1 : 0;
            not.CompileToMethod(method);
            return method;
        }

        public static MethodInfo Exit()
        {
            return typeof(Environment).GetMethod("Exit");
        }
    }
}
