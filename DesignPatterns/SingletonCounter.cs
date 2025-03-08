using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.DesignPatterns
{
    public sealed class SingletonCounter
    {
        private static int Counter = 0;
        private static SingletonCounter Instance = null;

        //Ensuring the class is Thread-safe requires an Instance Lock
        private static readonly object InstanceLock = new object();

        private SingletonCounter()
        {
            Counter++;
            Console.WriteLine($"Counter value: {Counter}");
        }

        public static SingletonCounter GetSingletonCounterInstance()
        {
            //if the current instance is null
            if(Instance == null)
            {
                //lock the thread to create a new instance 
                lock (InstanceLock)
                {
                    //create and return a new instance 
                    //prevents race condition from occuring
                    if (Instance == null)
                    {
                        Instance = new SingletonCounter();
                    }
                }
            }

            return Instance;
        }

        public void PrintDetails(string message)
        {
            Console.WriteLine($"Message: {message}");
        }
    }
}
