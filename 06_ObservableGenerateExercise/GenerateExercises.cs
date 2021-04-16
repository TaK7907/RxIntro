using System;
using System.Reactive.Linq;
using System.Linq;

namespace RxIntro.Step2
{
    class GenerateExercises
    {
        static IObservable<int> Method1(int initialValue)
        {
            return Observable.Generate<int,int>(
                initialValue,
                st => st == initialValue,
                x => x + 1,
                x => x);
        }
        static void Main(string[] args)
        {
            var s = Method1(2);
            s.Subscribe(
                x => Console.WriteLine(x),
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("complete"));
        }
    }
}
