using System;
using System.Threading;
public class Spinner
    {
        private const string Sequence = @"/-\|";
        private int counter = 0;
        private readonly int delay;
        private bool active;
        private readonly Thread thread;

        public Spinner(int delay = 100)
        {
            this.delay = delay;
            thread = new Thread(Spin);
        }

        public void Start()
        {
            active = true;
            if (!thread.IsAlive)
                thread.Start();
        }

        public void Stop()
        {
            active = false;
            Draw(' ');
        }

        private void Spin()
        {
            while (active)
            {
                Turn();
                Thread.Sleep(delay);
            }
        }

        private void Draw(char c)
        {
            // Write but returning at the beginning of the file thanks to the \r
            Console.Write("\r"+c);
        }

        private void Turn()
        {
            Draw(Sequence[++counter % Sequence.Length]);
        }

        public void Dispose()
        {
            Stop();
        }
    }

