using System;
using System.Reactive.Linq;

namespace RxIntro.Step2
{
    class Transitioning
    {
        static void FromDelegate()
        {
            static void FromAction()
            {
                var s = Observable.Start(
                    () => {
                        Console.WriteLine("Action started");
                        for(var i=0; i != 10; ++i)
                        {
                            Console.Write("a");
                            System.Threading.Thread.Sleep(100);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Action is being completed");
                    });

                s.Subscribe(
                    x => Console.WriteLine($"Action: {x}"),
                    () => Console.WteLine("Action: completed"));
            }

            static void FromFunc()
            {
                var s = Observable.Start(
                    () => {
                        Console.WriteLine("Function started");
                        for (var i = 0; i != 10; ++i)
                        {
                            Console.Write("f");
                            System.Threading.Thread.Sleep(100);
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

        static void Main(string[] args)
        {
            FromDelegate();

            Console.ReadLine();
        }
    }
}
