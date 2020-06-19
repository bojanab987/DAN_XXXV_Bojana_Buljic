using System;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {
        static int users;
        static int number;        
        static Random rnd = new Random();
        static object locker = new object();
        static Thread[] threads;
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
        /// Method where users guessing the number and writes the message to user if some one guessed
        /// </summary>
        static void GuessNumber()
        {
            lock (locker)
            {
                Thread.Sleep(100);
                int guess = rnd.Next(1, 101);
                Console.WriteLine("{0} tries with number {1}", Thread.CurrentThread.Name, guess);

                if (guess == number)
                {
                    lock (locker)
                    {
                        Console.WriteLine("“Thread_{0} wins, requested number is {1}", Thread.CurrentThread.Name, number);
                    }

                }
                else if (guess % 2 == number % 2)
                {
                    Console.WriteLine("{0} guessed the parity of number!", Thread.CurrentThread.Name);
                }
                else
                {
                    Console.WriteLine("You did not guesed the number nor its parity!");
                }

            }
        }

        static void Main(string[] args)
        {
            
            secondThread.Join();
            Console.ReadLine();
        }
    }
}
