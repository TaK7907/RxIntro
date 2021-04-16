using System;
using System.Reactive.Linq;

namespace RxIntro.Step2
{
    class TimerSequences
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

        static void NonBlocking_event_driven_improved1()
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

        static void NonBlocking_event_driven_another()
        {
            var ob = Observable.Create<string>(
                observer =>
                {
                    var timer = new System.Timers.Timer();
                    timer.Enabled = true;
                    timer.Interval = 100;
                    timer.Elapsed += OnTimerElapsed;
                    timer.Start();
                    return ()=>{
                        timer.Elapsed -= OnTimerElapsed;
                        timer.Dispose();
                    };
                });
            var subscription = ob.Subscribe(Console.WriteLine);
            Console.ReadLine();
            subscription.Dispose();
        }

        static private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime);
        }

        static void Main(string[] args)
        {
            //NonBlocking_event_driven();

            //NonBlocking_event_driven_improved1();

            NonBlocking_event_driven_another();


            Console.ReadLine();
        }
    }
}
