using CustomEventArguments.Models;
using EventFirstLook.Models;
using System;

namespace CustomEventArguments
{
    class Program
    {
        static void Main(string[] args)
        {
            Car c1 = new Car("SlugBug", 100, 10);

            // Register event handlers.
            c1.AboutToBlow += CarIsAlmostDoomed;
            c1.AboutToBlow += CarAboutToBlow;

            EventHandler<CarEventArgs> d = CarExploded;
            c1.Exploded += d;

            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
            {
                c1.Accelerate(20);
            }

            // Remove CarExploded method from invocation list.
            c1.Exploded -= d;
            Console.WriteLine("\n***** Speeding up *****");
            for (int i = 0; i < 6; i++)
            {
                c1.Accelerate(20);
            }

            Console.ReadLine();
        }

        static void CarAboutToBlow(object sender, CarEventArgs e)
        {
            // Just to be safe, perform a runtime check before casting.
            if (sender is Car c)
            {
                Console.WriteLine($"Critical Message from {c.PetName}: {e.msg}");
            }
        }

        static void CarIsAlmostDoomed(object sender, CarEventArgs e)
        {
            Console.WriteLine("\n=> Critical Message from Car: {0}", e.msg);
        }

        static void CarExploded(object sender, CarEventArgs e)
        {
            Console.WriteLine(e.msg);
        }
    }
}