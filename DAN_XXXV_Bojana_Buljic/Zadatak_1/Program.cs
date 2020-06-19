using System;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {
        // number of persons to guess numbers
        static int users;
        //Number user selected for guessing
        static int number;       
        static Random rnd = new Random();
        static Thread secondThread;

        /// <summary>
        /// Static method for Generating/Selecting number for guessing and asking for user's input about number of persons to play
        /// </summary>
        static void GenerateGuessingNo()
        {
            Console.WriteLine("How many persons will be guessing the number?");
            bool success = int.TryParse(Console.ReadLine(), out users);
            while (!success || users <= 0 || users > 5000)
            {
                Console.WriteLine("Input is not valid. Minimum number of persons to play is 1 and maximum is 5000. Please try again.");
                success = int.TryParse(Console.ReadLine(), out users);
            }
            Console.WriteLine("Please enter number for guess: [1-100]");
            bool success2 = int.TryParse(Console.ReadLine(), out number);
            while (!success2 || number < 1 || number > 100)
            {
                Console.WriteLine("Input is not valid. Enter number 1-100. Please try again.");
                success2 = int.TryParse(Console.ReadLine(), out number);
            }
            //Starting generating the threads for guessing numbers
            secondThread = new Thread(new ThreadStart(ThreadGenerator));
            secondThread.Start();
            //Treba da zapocne drugi thread
            Console.WriteLine("You selected {0} persons to guess the number.", users);
            Console.WriteLine("Number for guessing is selected!\nYou can start guessing.\n");

        }

        static void Main(string[] args)
        {
            Thread firstThread = new Thread(new ThreadStart(GenerateGuessingNo));
            firstThread.Start();
            firstThread.Join();
            Console.ReadLine();
        }
    }
}
