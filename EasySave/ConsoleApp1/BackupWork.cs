using System;

namespace consoleApp
{
    public class BackupJob
    {
        private String name;
        private String source;
        private String destination;
        private Boolean isFull;

        // Getters and setter for each attribute of the backup job
        public string Name { get => name; set => name = value; }
        public string Source { get => source; set => source = value; }
        public string Destination { get => destination; set => destination = value; }
        public Boolean IsFull { get => isFull; set => isFull = value; }

        public BackupJob(String Name, String Source, String Destination, Boolean IsFull)
        {
            //TODO:adding check if folder is accessible
            if (Name.Length >= 1 && Source.Length >= 1 && Destination.Length >= 1)
            {
                this.name = Name;
                this.source = Source;
                this.destination = Destination;
                this.isFull = IsFull;
            }
            else
            {
                throw new System.ArgumentException("Parameter cannot be empty", "original");
            }

        }


    }
}
