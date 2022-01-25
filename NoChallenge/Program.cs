using System;
using System.IO;
using System.Reflection;
using HarmonyLib;

namespace NoChallenge
{
    internal static class Program
    {
        private const char Key = '§';

        private static void Main(string[] args)
        {
            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"File not found: {args[0]}");
                return;
            }

            Console.Title = "Dont use string.Equals :D";

            var assembly = Assembly.LoadFile(Path.GetFullPath(args[0]));

            var paraminfo = assembly.EntryPoint.GetParameters();
            
            object[] parameters = new object[paraminfo.Length];

            InstallHook();

            assembly.EntryPoint.Invoke(null, parameters);

            Console.ReadKey();
        }

        private static void InstallHook()
        {
            var target = typeof(string).GetMethod("Equals", new[] {typeof(string), typeof(string)});

            if (target == null)
                throw new Exception("Could not resolve string.Equals");

            var harmony = new Harmony("NoChallenge");
            
            var stub = typeof(Program).GetMethod("Prefix");

            harmony.Patch(target, new HarmonyMethod(stub));

            Console.WriteLine($"Installed hook on {target}");
        }
        
        [HarmonyPatch(typeof(string), nameof(string.Equals))]
        public static bool Prefix(string a, string b)
        {
            if (a is null || b is null)
                return true;

            if (a.Length == 0 || b.Length == 0)
                return true;

            if (a[0] != Key && b[0] != Key) 
                return true;

            string solution = a[0] == Key ? b : a;

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Solution: {0}", solution);
            File.WriteAllText("solution.txt", solution);

            return true;
        }
    }
}