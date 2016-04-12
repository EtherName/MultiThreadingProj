using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadingProj
{
    class Program
    {
        static Queue<string> _queue = new Queue<string>();
       
        static void Main(string[] args)
        {
            Thread thread = new Thread(SetMessage);
            Thread thread1 = new Thread(SendMessage);
            thread.Start();
            thread1.Start();
            Console.ReadLine();
        }


        static void SetMessage()
        {
            while (true)
            {
                _queue.Enqueue(Console.ReadLine());
            }
        }

        static void SendMessage()
        {
            while (true)
            {
                Console.WriteLine($"\n-----------Sending {_queue.Count} message !-------------");
                while (_queue.Count > 0)
                {
                    _queue.Dequeue();
                }
                Thread.Sleep(5000);

            }
        }
            

    }
}
