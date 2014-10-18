using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAppConsole.AsyncSample
{
    public class AsyncProgramSample
    {
        static int GetPrimesCount(int start, int count)
        {
            return 
                ParallelEnumerable.Range(start, count).Count(n => 
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));
        }

        static Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() =>
                ParallelEnumerable.Range(start, count).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }

        public static void DisplayPrimeCounts()
        {
            for (int i = 0; i < 10; i++)
                Console.WriteLine(GetPrimesCount(i * 1000000 + 2, 1000000) +
                " primes between " + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1));
            Console.WriteLine("Done!");
         }

        public static void DisplayPrimeCounts_2()
        {
      
            for (int i = 0; i < 10; i++)
            {
                var awaiter = GetPrimesCountAsync(i * 1000000 + 2, 1000000).GetAwaiter();
                awaiter.OnCompleted(() => Console.WriteLine(awaiter.GetResult() + " primes between... " + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1)));
            }
            Console.WriteLine("Done");
        }

        public static void DisplayPrimeCounts_3()
        {
            DisplayPrimeCountsFrom(0);
        }

        static void DisplayPrimeCountsFrom(int i)
        {
            var awaiter = GetPrimesCountAsync(i * 1000000 + 2, 1000000).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Console.WriteLine(awaiter.GetResult() + " primes between..." + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1));
                if (i++ < 10) DisplayPrimeCountsFrom(i);
                else Console.WriteLine("Done");
            });
        }
    }

    public class PrimesStateMachine
    {
        static Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() =>
                ParallelEnumerable.Range(start, count).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }

        TaskCompletionSource<object> _tcs = new TaskCompletionSource<object>();
        
        public Task Task { get { return _tcs.Task; } }
        
        public void DisplayPrimeCountsFrom (int i)
        {
            var awaiter = GetPrimesCountAsync (i*1000000+2, 1000000).GetAwaiter();
            awaiter.OnCompleted (() =>
            {
                Console.WriteLine (awaiter.GetResult());
                if (i++ < 10) DisplayPrimeCountsFrom (i);
                else { Console.WriteLine ("Done"); _tcs.SetResult (null); }
            });
        }
    }
}
