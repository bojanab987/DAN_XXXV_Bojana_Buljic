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
