using System;
using System.Threading;
public static class Spinner
    {
       // Sequence = regex of character used for this animation
        private const string Sequence = @"/-\|";

        // To make it turn
        private static int counter = 0;

        // The user need to see the animation so there is a delay each time our character turns
        private static readonly int delay = 100;

       // Boolean to know if we can start / stop it
        private static bool active;

        // Asynchronous work
        private static Thread thread;

        static Spinner()
        {
            thread = new Thread(Spin);
        }

        // Start the spinner by updating the boolean and calling start
        public static void Start()
        {
            active = true;
            thread = new Thread(Spin);
            if (!thread.IsAlive)
                thread.Start();
        }

        // Stopping the spinner by removing the character
        public static void Stop()
        {
            active = false;
            Draw(' ');
        }

        private static void Spin()
        {
        // While the spinneer is activated, turn it and beetween each rotation wait 100ms (the delay)
            while (active)
            {
                Turn();
                Thread.Sleep(delay);
            }
        }

        private static void Draw(char c)
        {
            // Show the spinner and rewrite it thanks to the \r
            Console.Write("\r"+c);
        }

        private static void Turn()
        {
            // This will just rotate the spinner based on our sequence characters ( \ | / ...)
            Draw(Sequence[++counter % Sequence.Length]);
        }
        
        // Remove the spinner
        public static void Dispose()
        {
            Stop();
        }
    }

