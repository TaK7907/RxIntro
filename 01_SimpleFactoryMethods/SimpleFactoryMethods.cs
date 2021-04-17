using System;
using System.Reactive.Linq;

namespace RxIntro.Step2
{
    class SimpleFactoryMethods
    {
        static void Return_FactoryMethod()
        {
            Console.WriteLine($"---{nameof(Return_FactoryMethod)}---");

            // 1. Sequence declaration
            var singleValue = Observable.Return<string>("Value");

            // 2. Sequence subscription declaration
            var observer = singleValue.Subscribe(
                x => Console.WriteLine(x),
                ex => Console.WriteLine($"Error {ex.Message}"),
                () => Console.WriteLine("Sequence completed"));
        }

        static void Empty_FactoryMethod()
        {
            Console.WriteLine($"---{nameof(Empty_FactoryMethod)}---");

            // 1. Sequence declaration
            var empty = Observable.Empty<string>("Value");

            // 2. Sequence subscription declaration
            var observer = empty.Subscribe(
                x => Console.WriteLine(x),
                ex => Console.WriteLine($"Error {ex.Message}"),
                () => Console.WriteLine("Sequence completed"));
        }

        static void Never_FactoryMethod()
        {
            Console.WriteLine($"---{nameof(Never_FactoryMethod)}---");

            // 1. Sequence declaration
            var never = Observable.Never<string>("Value");

            // 2. Sequence subscription declaration
            var observer = never.Subscribe(
                x => Console.WriteLine(x),
                ex => Console.WriteLine($"Error {ex.Message}"),
                () => Console.WriteLine("Sequence completed"));
        }

        static void Throw_FactoryMethod()
        {
            Console.WriteLine($"---{nameof(Throw_FactoryMethod)}---");

            // 1. Sequence declaration
            var throws = Observable.Throw<string>(new Exception());

            // 2. Sequence subscription declaration
            var observer = throws.Subscribe(
                x => Console.WriteLine(x),
                ex => Console.WriteLine($"Error {ex.Message}"),
                () => Console.WriteLine("Sequence completed"));
        }

        static IObservable<string> BlockingMethod()
        {
            Console.WriteLine($"---{nameof(BlockingMethod)}---");

            var subject = new System.Reactive.Subjects.ReplaySubject<string>();
            subject.OnNext("a");
            subject.OnNext("b");
            subject.OnCompleted();
            System.Threading.Thread.Sleep(5000);
            return subject;
        }
        static IObservable<string> NonBlocking()
        {
            Console.WriteLine($"---{nameof(NonBlocking)}---");

            return Observable.Create<string>(
                observer => {
                    observer.OnNext("a");
                    observer.OnNext("b");
                    observer.OnCompleted();
                    System.Threading.Thread.Sleep(5000);
                    //return System.Reactive.Disposables.Disposable.Create(
                    //    () => Console.WriteLine("Observer has unsubscribed"));
                    return () => Console.WriteLine("Observer has unsubscribed");
                });
        }

        static void Create_FactoryMethod()
        {
            var s1 = BlockingMethod();
            s1.Subscribe(
                x => Console.WriteLine(x),
                ex => Console.WriteLine($"Error {ex.Message}"),
                () => Console.WriteLine("Sequence completed"));

            var s2 = NonBlocking();
            s2.Subscribe(
                x => Console.WriteLine(x),
                ex => Console.WriteLine($"Error {ex.Message}"),
                () => Console.WriteLine("Sequence completed"));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Simple factory methods");
            Return_FactoryMethod();

            Console.WriteLine();
            Empty_FactoryMethod();

            Console.WriteLine();
            Never_FactoryMethod();

            Console.WriteLine();
            Throw_FactoryMethod();

            Console.WriteLine();
            Create_FactoryMethod();

            Console.WriteLine();
            Console.WriteLine("End");
        }
    }
}
