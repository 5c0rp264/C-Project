using System;

namespace consoleApp
{
    public class BackupWork
    {
        private String name;
        private String source;
        private String destination;


        public string Name { get => name; set => name = value; }
        public string Source { get => source; set => source = value; }
        public string Destination { get => destination; set => destination = value; }

        public BackupWork(String Name, String Source, String Destination)
        {
            //TODO:adding check if folder is accessible
            if (Name.Length >= 1 && Source.Length >= 1 && Destination.Length >= 1)
            {
                this.name = Name;
                this.source = Source;
                this.destination = Destination;
            }
            else
            {
                throw new System.ArgumentException("Parameter cannot be empty","original");
            }
            
        }


    }
}
