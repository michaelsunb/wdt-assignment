using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to MoSS");
            Console.WriteLine("===============");

            Factory factory = new Factory();
            IOption[] options = new IOption[4];
            options[0] = factory.OptionA();
            options[1] = factory.OptionB();
            options[2] = factory.OptionC();
            options[3] = factory.Exit();

            int selectOption = 0;
            while (selectOption < options.Length)
            {
                selectOption = DisplayOption(options);
            }
        }

        private static int DisplayOption(IOption[] options)
        {
            int i = 0;
            foreach (IOption creator in options)
            {
                Console.WriteLine(++i + ". {0}", creator.GetOption());
            }

            try
            {
                // Read user input
                int line = int.Parse(Console.ReadLine());
                options[--line].Selected();

                return line;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Please enter a number from 1 to " + options.Length);
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a number!");
            }
            return 0;
        }
    }
}
