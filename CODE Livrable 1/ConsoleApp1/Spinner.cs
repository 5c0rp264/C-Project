using System;
using System.Threading;
public class Spinner
    {
       // Sequence = regex of character used for this animation
        private const string Sequence = @"/-\|";

        // To make it turn
        private int counter = 0;

        // The user need to see the animation so there is a delay each time our character turns
        private readonly int delay;

       // Boolean to know if we can start / stop it
        private bool active;

        // Asynchronous work
        private readonly Thread thread;

        public Spinner(int delay = 100)
        {
            this.delay = delay;
            thread = new Thread(Spin);
        }

        // Start the spinner by updating the boolean and calling start
        public void Start()
        {
            active = true;
            if (!thread.IsAlive)
                thread.Start();
        }

        // Stopping the spinner by removing the character
        public void Stop()
        {
            active = false;
            Draw(' ');
        }

        private void Spin()
        {
        // While the spinneer is activated, turn it and beetween each rotation wait 100ms (the delay)
            while (active)
            {
                Turn();
                Thread.Sleep(delay);
            }
        }

        private void Draw(char c)
        {
            // Show the spinner and rewrite it thanks to the \r
            Console.Write("\r"+c);
        }

        private void Turn()
        {
            // This will just rotate the spinner based on our sequence characters ( \ | / ...)
            Draw(Sequence[++counter % Sequence.Length]);
        }
        
        // Remove the spinner
        public void Dispose()
        {
            Stop();
        }
    }

