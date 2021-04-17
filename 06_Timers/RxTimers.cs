using System;
using System.Reactive.Linq;

namespace RxIntro.Step2
{
    class RxTimers
    {
        static void By_Observable_Interval()
        {
            var interval = Observable.Interval(TimeSpan.FromMilliseconds(250));
            var d = interval.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("completed"));

            Console.ReadLine();
            d.Dispose();
        }

        static void By_Observable_Timer()
        {
            var timer = Observable.Timer(TimeSpan.FromSeconds(1));
            var d = timer.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("completed"));
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"---{nameof(By_Observable_Interval)}---");
            By_Observable_Interval();

            Console.WriteLine($"---{nameof(By_Observable_Timer)}---");
            By_Observable_Timer();


            Console.WriteLine("Hit enter to quit");
            Console.ReadLine();
        }
    }
}
