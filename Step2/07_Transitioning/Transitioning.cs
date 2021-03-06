using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Reactive.Concurrency;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;

namespace RxIntro.Step2
{
    class Transitioning
    {
        static System.Timers.Timer _timer;

        static void FromDelegate()
        {
            static void FromAction()
            {
                var s = Observable.Start(
                    () =>
                    {
                        Console.WriteLine("Action started");
                        for (var i = 0; i != 10; ++i)
                        {
                            Console.Write("a");
                            Thread.Sleep(100);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Action is being completed");
                    });

                s.Subscribe(
                    x => Console.WriteLine($"Action: {x}"),
                    () => Console.WriteLine("Action: completed"));
            }

            static void FromFunc()
            {
                var s = Observable.Start(
                    () =>
                    {
                        Console.WriteLine("Function started");
                        for (var i = 0; i != 10; ++i)
                        {
                            Console.Write("f");
                            Thread.Sleep(100);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Function is being completed");
                        return 1;
                    });

                s.Subscribe(
                    x => Console.WriteLine($"Function: {x}"),
                    () => Console.WriteLine("Function: completed"));
            }

            FromAction();
            FromFunc();
        }

        static void FromEventPattern()
        {
            var s = Observable.FromEventPattern<ElapsedEventHandler, ElapsedEventArgs>(
                h => h.Invoke,
                h => _timer.Elapsed += h,
                h => _timer.Elapsed -= h);
            var d = s.Subscribe(
                x => Console.WriteLine($"FromEventPattern: {x.EventArgs.SignalTime}"),
                ex => Console.WriteLine($"FromEventPattern: {ex.Message}"),
                () => Console.WriteLine("FromEventPattern: completed"));
        }

        static void FromEvent()
        {
            var s1 = Observable.FromEvent<ElapsedEventHandler, ElapsedEventArgs>(
                h => (s, e) => h(e),
                h => _timer.Elapsed += h,
                h => _timer.Elapsed -= h);
            var d1 = s1.Subscribe(
                x => Console.WriteLine($"FromEvent: {x.SignalTime}"),
                ex => Console.WriteLine($"FromEvent: {ex.Message}"),
                () => Console.WriteLine("FromEvent: completed"));
        }

        static void FromTask()
        {
            Console.WriteLine($"{nameof(FromTask)}: (TID:{Thread.CurrentThread.ManagedThreadId})");
            var t = Task.Run(() =>
            {
                Thread.Sleep(500);
                return $"FromTaskTest (TID:{Thread.CurrentThread.ManagedThreadId})";
            });
            var s = t.ToObservable();
            var d = s.Subscribe(Console.WriteLine,
                () => Console.WriteLine($"End (TID:{Thread.CurrentThread.ManagedThreadId})"));
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"{nameof(FromDelegate)}");
            FromDelegate();

            Task.Delay(2000).Wait();
            Console.WriteLine();

            Console.WriteLine($"{nameof(FromEventPattern)} and {nameof(FromEvent)}");
            using (_timer = new System.Timers.Timer(1000))
            {
                _timer.Start();
                FromEventPattern();
                FromEvent();
                Task.Delay(5000).Wait();
                _timer.Stop();
            }
            Console.WriteLine();

            FromTask();
            Task.Delay(2000).Wait();
        }
    }
}
