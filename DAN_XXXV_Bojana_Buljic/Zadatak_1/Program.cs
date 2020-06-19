using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        static int users;
        static int number;       
        static Random rnd = new Random();

        /// <summary>
        /// Static method for Generating random number for guesing and asking for user's input about number of persons to play
        /// </summary>
        static void GenerateGuessingNo()
        {
            Console.WriteLine("How many persons will be guessing the number?");
            bool success = int.TryParse(Console.ReadLine(), out users);
            while (!success || users <= 0)
            {
                Console.WriteLine("Input is not valid. You need to enter number greater than 0. Please try again.");
                success = int.TryParse(Console.ReadLine(), out users);
            }
            Console.WriteLine("Generating number...\n");
            number = rnd.Next(1, 101);            

            Console.WriteLine("You selected {0} persons to guess the number.", users);
            Console.WriteLine("Number for guessing is generated!\nYou can start guessing.");

        }

        static void Main(string[] args)
        {
        }
    }
}
