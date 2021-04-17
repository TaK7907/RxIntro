using System;
using System.Reactive.Linq;
using System.Linq;

namespace RxIntro.Step2
{
    class GenerateExercises
    {
        static void GenerateExerciseOne()
        {
            static IObservable<int> Return(int initialValue)
            {
                return Observable.Generate(
                    initialValue,
                    st => st == initialValue,
                    x => x + 1,
                    x => x);
            }

            var s = Return(2);
            s.Subscribe(
                x => Console.WriteLine($"return: {x}"),
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("complete"));

        }

        /// <summary>
        /// Created my own Range method using <c>Observable.Generate</c>
        /// </summary>
        static void RangeWithGenerate()
        {
            static IObservable<int> Range(int start, int count)
            {
                return Observable.Generate(
                    start,
                    n => n < start + count,
                    k => k + 1,
                    x => x);
            }

            var s = Range(1, 10);
            s.Subscribe(
                x => Console.WriteLine($"range: {x}"),
                ex => Console.WriteLine(ex),
                () => Console.WriteLine("complete"));
        }
        
        static void Exercise3()
        {
            static IObservable<int> RandomSequence(int count)
            {
                var rand = new System.Random();
                return Observable.Generate(
                    1,
                    x => x <= count,
                    x => x + 1,
                    _ => rand.Next(-20, 21)
                    );
            }

            var s = RandomSequence(6);
            s.Subscribe(
                x => Console.WriteLine($"random: {x}"),
                () => Console.WriteLine("complete"));
        }

        static void Main(string[] args)
        {
            GenerateExerciseOne();
            Console.WriteLine();

            RangeWithGenerate();
            Console.WriteLine();

            Exercise3();
            Console.WriteLine();
        }
    }
}
