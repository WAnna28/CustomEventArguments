using CustomEventArguments.Models;
using System;

namespace EventFirstLook.Models
{
    // With this example, you have configured the car to send two custom events without having to define custom
    // registration functions or declare delegate member variables.

    // Defining an event is a two-step process. First, you need to define a delegate type (or reuse an existing one)
    // that will hold the list of methods to be called when the event is fired. Next, you declare an event
    // (using the C# event keyword) in terms of the related delegate type.
    public class Car
    {
        // Internal state data.
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; } = 100;
        public string PetName { get; set; }

        // Is the car available or sold out?
        private bool _carIsSoldOut;

        // Class constructors.
        public Car() { }
        public Car(string name, int maxSp, int currSp)
        {
            CurrentSpeed = currSp;
            MaxSpeed = maxSp;
            PetName = name;
        }

        // This delegate works in conjunction with the Car's events.
        public delegate void CarEngineHandler(string msgForCaller);

        // This car can send these events.
        public event EventHandler<CarEventArgs> Exploded;
        public event EventHandler<CarEventArgs> AboutToBlow;

        // Just fire out the Exploded notification.
        public void Accelerate(int delta)
        {
            // If the car is sold out, fire Exploded event.
            if (_carIsSoldOut)
            {
                Exploded?.Invoke(this, new CarEventArgs("Sorry, this car is sold out..."));
            }
            else
            {
                CurrentSpeed += delta;
                // Almost sold out?
                if (10 == MaxSpeed - CurrentSpeed)
                {
                    AboutToBlow?.Invoke(this, new CarEventArgs("Careful buddy! Gonna blow!"));
                }

                // Still OK!
                if (CurrentSpeed >= MaxSpeed)
                {
                    _carIsSoldOut = true;
                }
                else
                {
                    Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
                }
            }
        }
    }
}