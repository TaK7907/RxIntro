using System;
using System.Reactive.Linq;

namespace _03_Timers
{
    class Timers
    {
        static void NonBlocking_event_driven()
        {
            var ob = Observable.Create<string>(
                observer =>
                {
                    var timer = new System.Timers.Timer();
                    timer.Interval = 1000;
                    timer.Elapsed += (s, e) => observer.OnNext("tick");
                    timer.Elapsed += OnTimerElapsed;
                    timer.Start();
                    return System.Reactive.Disposables.Disposable.Empty;
                });
            var subscription = ob.Subscribe(Console.WriteLine);
            Console.ReadLine();
            subscription.Dispose();
        }

        static void FixedNonBlocking_event_driven()
        {
            var ob = Observable.Create<string>(
                observer =>
                {
                    var timer = new System.Timers.Timer();
                    timer.Interval = 1000;
                    timer.Elapsed += (s, e) => observer.OnNext("tick");
                    timer.Elapsed += OnTimerElapsed;
                    timer.Start();
                    return timer;
                });
            var subscription = ob.Subscribe(Console.WriteLine);
            Console.ReadLine();
            subscription.Dispose();
        }

        private static void OnTimerElapsed(object sender,System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime);
        }

        static void Main(string[] args)
        {
            //NonBlocking_event_driven();

            FixedNonBlocking_event_driven();
        }
    }
}
