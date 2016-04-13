using System;
using System.Collections.Concurrent;
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
        static object _check = false;
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
            string read = "";
            while (true)
            {
                read = Console.ReadLine();

                if (read.ToUpper().Equals("EXIT"))
                {
                    lock(_check)
                    {
                        _check = true;
                        Thread.CurrentThread.Abort();
                    }
                }
                _queue.Enqueue(read);
            }
        }

        static void SendMessage()
        {
            while (true)
            {
                Thread.Sleep(5000);
                int count = _queue.Count;
                int _count;

                if (count == 0)
                {
                    _count = 0;
                }
                else if (count <= 3)
                {
                    _count = 1;
                }
                else
                {
                    if (count % 3 >= 1)
                    {
                        _count = count / 3 + 1;
                    }
                    else
                    {
                        _count = count / 3;
                    }
                }
                int _count2 = _count;
                //_count = count > 3 ? (count / 3 + count % 3 >= 1 ? 1 : 0) : 1;
                while (_count-- > 0)
                {
                    new Thread(SendThreeMessage).Start(3);
                }
                lock (_check)
                {
                    if (_check.Equals(true))
                        Thread.CurrentThread.Abort();
                }
                Console.WriteLine($"\n------Sending {count} message !------{_count2} thread started------");
            }
        }
        static void SendThreeMessage(Object stateInfo)
        {
            int _count = (int)stateInfo;
            while (_queue.Count > 0 && _count-- > 0)
            {
                _queue.Dequeue();
            }
            Thread.CurrentThread.Abort();
        }
    }
}
