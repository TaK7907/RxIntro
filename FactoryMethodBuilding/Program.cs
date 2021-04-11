using System;
using System.Reactive.Linq;

namespace RxIntro.Step2
{
    class FactoryMethodBuilding
    {
        private static string BuildLog<T>(T s)
        {
            var t = DateTime.Now;

            var b = new System.Text.StringBuilder();
            b.AppendFormat($"{t.Minute:d02}:{t.Second:d02}.{t.Millisecond:d03} ");
            b.Append(s);
            b.AppendFormat($" (ThreadID {System.Threading.Thread.CurrentThread.ManagedThreadId})");

            return b.ToString();
        }

        private static void Print<T>(T s)
        {
            Console.WriteLine(BuildLog(s));
        }

        private static IObservable<T> Empty<T>()
        {
            return Observable.Create<T>(
                (IObserver<T> observer) => {
                    observer.OnCompleted();
                    System.Threading.Thread.Sleep(1000);
                    return System.Reactive.Disposables.Disposable.Create(
                        () => Print("Observer has unsubscrived"));
                });
        }

        private static IObservable<T> Return<T>(T value)
        {
            return Observable.Create<T>(
                (IObserver<T> observer) => {
                    observer.OnNext(value);
                    observer.OnCompleted();
                    System.Threading.Thread.Sleep(1000);
                    return System.Reactive.Disposables.Disposable.Create(
                        () => Print("Observer has unsubscrived"));
                });
        }

        private static IObservable<T> Never<T>()
        {
            return Observable.Create<T>(
                (IObserver<T> observer) => {
                    System.Threading.Thread.Sleep(1000);
                    return System.Reactive.Disposables.Disposable.Create(
                        () => Print("Observer has unsubscribed"));
                });
        }

        private static IObservable<T> Throw<T>()
        {
            return Observable.Create<T>(
                (IObserver<T> observer) => {
                    observer.OnError(new Exception("Error has occurred"));
                    System.Threading.Thread.Sleep(1000);
                    return System.Reactive.Disposables.Disposable.Create(
                        () => Print("Observer has unsubscribed"));
                });
        }

        private static void LetSubscrive<T>(IObservable<T> s)
        {
            s.Subscribe(
                x => Print(x),
                ex => Print(ex.Message),
                () => Print("Sequence Completed"));
        }

        static void Main(string[] args)
        {
            Print("Main started.");

            Console.WriteLine();
            Print("---Empty---");

            var s1 = Empty<string>();
            LetSubscrive(s1);

            Console.WriteLine();
            Print("---Return---");

            var s2 = Return("Hello");
            LetSubscrive(s2);

            Console.WriteLine();
            Print("---Never---");

            var s3 = Never<string>();
            LetSubscrive(s3);
            
            Console.WriteLine();
            Print("---Throw---");

            var s4 = Throw<string>();
            LetSubscrive(s4);
        }    }
}
