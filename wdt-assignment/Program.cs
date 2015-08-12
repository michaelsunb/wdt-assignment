using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;

namespace wdt_assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory();
            Type myType =(typeof(Factory));

            // An array of factory methods using reflection for polymorphism
            MethodInfo[] creators = myType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            // Iterate over creators and create products 
            Display(factory, creators);

            // Read user input
            int line = int.Parse(Console.ReadLine());
            ((IOption)creators[--line].Invoke(factory, null)).Selected();
            Console.ReadKey();
        }

        private static void Display(Factory factory, MethodInfo[] creators)
        {
            int i = 0;
            Console.WriteLine("Welcome to MoSS");
            Console.WriteLine("===============");
            foreach (MethodInfo creator in creators)
            {
                IOption product = (IOption)creator.Invoke(factory, null);
                Console.WriteLine(++i + ". {0}", product);
            }
        }
    }
}
