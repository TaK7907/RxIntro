using System;
using System.Reactive.Linq;
using System.Linq;
using System.Collections.Generic;

namespace RxIntro.Step2
{
    class Corecursion
    {
        private static IEnumerable<T> Unfold<T>(T seed, Func<T, T> accumulator)
        {
            var nextValue = seed;
            while (true)
            {
                yield return nextValue;
                nextValue = accumulator(nextValue);
            }
        }

        public static IObservable<int> Range_FactoryMethod(int start, int count)
        {
            return Observable.Generate(
                start,
                x => x < start + count,
                x => x + 1,
                x => x);
        }

        static void Main(string[] args)
        {
            //var naturalNumbers = Unfold(1, i => i + 1);
            //Console.WriteLine("1st 10 Natural numbers");
            //foreach(var naturalNumber in naturalNumbers.Take(10))
            //{
            //    Console.WriteLine(naturalNumber);
            //}

            var s = Range_FactoryMethod(15, 10);
            s.Subscribe(Console.WriteLine);
        }
    }
}
