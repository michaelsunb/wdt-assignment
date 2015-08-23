using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdt_assignment.model;

namespace wdt_assignment
{
    class Program
    {
        private const int FOUR_OPTIONS = 4;
        private const int OPTION_A_DISPLAY_CINEPLEX_LIST = 0;
        private const int OPTION_B_SEARCH_CINEPLEX_MOVIE = 1;
        private const int OPTION_C_EDIT_DELETE_BOOKING = 2;
        private const int OPTION_D_EXIT = 3;
        /// <summary>Main method to start up the program.</summary>
        /// <param name="args"> parameter takes an array of arguements.</param>
        /// <returns>Returns the console screen of the program.</returns>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to MoSS");
            Console.Write("===============");

            JsonModel jsonMovie = new JsonModel();
            jsonMovie.LoadJsonDetails();

            Factory factory = new Factory();
            IOption[] options = new IOption[FOUR_OPTIONS];
            options[OPTION_A_DISPLAY_CINEPLEX_LIST] = factory.OptionA;
            options[OPTION_B_SEARCH_CINEPLEX_MOVIE] = factory.OptionB;
            options[OPTION_C_EDIT_DELETE_BOOKING] = factory.OptionC;
            options[OPTION_D_EXIT] = factory.Exit;

            int selectOption = 0;
            while (selectOption < options.Length)
            {
                selectOption = DisplayOption(options);
            }
        }

        /// <summary>Displays the options set by the parameters and asks for an input for the user.</summary>
        /// <param name="options"> parameter takes an array of IOption interface.</param>
        /// <returns>Displays the options and returns input of the user.</returns>
        private static int DisplayOption(IOption[] options)
        {
            Console.WriteLine();

            int i = 0;
            foreach (IOption creator in options)
            {
                Console.WriteLine(++i + ". {0}", creator.GetOption());
            }

            try
            {
                // Read user input
                int line = EnterOption();
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
            catch (CustomCouldntFindException e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        /// <summary>Asks the user for an integer input. Must catch the Exception.</summary>
        /// <returns>Returns input of the user which should be a number. Otherwise catch the Exception.</returns>
        public static int EnterOption()
        {
            Console.Write("\nEnter an option: ");
            int line = int.Parse(Console.ReadLine());
            return line;
        }
    }
}
