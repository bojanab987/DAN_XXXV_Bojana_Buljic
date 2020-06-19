using System;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {
         // number of persons to guess numbers
        static int users;
        //Randomly generated number that need to be guessed
        static int number;
        //object to lock
        static readonly object locker = new object();
        //array of threads
        static Thread[] threads;
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

        /// <summary>
        /// Method creates users and generates threads for each user, gives name to them
        /// </summary>
        static void ThreadGenerator()
        {
            threads = new Thread[users];

            //loop for creating thread for each user and give the name to the thread
            for (int i = 0; i < threads.Length; i++)
            {
                Thread t = new Thread(new ThreadStart(GuessNumber));
                threads[i] = t;
                threads[i].Name = string.Format("User_{0}", i + 1);
            }

            //strating of threads (which performs GuessNumber) from thread array
            foreach (var i in threads)
            {
                i.Start();
            }

        }

        /// <summary>
        /// Method where users guessing the number and writes the message to user if some one guessed.
        /// Guessing performed until someone guess the number selected on the start
        /// </summary>
        static void GuessNumber()
        {
            int guess = 0;
            while (guess != number)
            {
                guess = rnd.Next(1, 101);

                lock (locker)
                {

                    Thread.Sleep(100);

                    Console.WriteLine("{0} tries with number {1}\n", Thread.CurrentThread.Name, guess);

                    if (guess % 2 == number % 2)
                    {
                        Console.WriteLine("{0} guessed the parity of number!\n", Thread.CurrentThread.Name);
                    }
                    if (guess == number)
                    {

                        Console.WriteLine("{0} wins, requested number is {1}\n", Thread.CurrentThread.Name, number);
                        Console.ReadLine();
                        Environment.Exit(0);
                    }

                }
            }
        }

        static void Main(string[] args)
        {
            Thread firstThread = new Thread(new ThreadStart(GenerateGuessingNo));
            firstThread.Start();
            firstThread.Join();
            secondThread.Join();
            Console.ReadLine();
        }
    }
}
