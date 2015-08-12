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
        private ArrayList ItemList;
        static void Main(string[] args)
        {
            Type myType =(typeof(Factory));
            // An array of factory methods using reflection
            // for polymorphism
            MethodInfo[] creators = myType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            // Iterate over creators and create products 
            Display(creators);
            string line = Console.ReadLine();
            Console.WriteLine("{0}", line);
            Console.ReadKey();
        }

        private static void Display(MethodInfo[] creators)
        {
            Factory factory = new Factory();
            int i = 0;
            foreach (MethodInfo creator in creators)
            {
                i++;
                Display product = (Display)creator.Invoke(factory, null);
                Console.WriteLine(i + ": {0}", product);
            }
        }
    }

    // "Product" 
    interface Display
    {
        string ToString();
    }

    // "ConcreteProductA" 
    class ConcreteProductA : Display
    {
        public override string ToString()
        {
            return "ConcreteProductA tostring";
        }
    }

    // "ConcreteProductB" 
    class ConcreteProductB : Display
    {
        public override string ToString()
        {
            return "ConcreteProductB tostring";
        }
    }

    // "ConcreteCreator" 
    class Factory
    {
        public Display GetProductA()
        {
            return new ConcreteProductA();
        }
        public Display GetProductB()
        {
            return new ConcreteProductB();
        }
    }
}
