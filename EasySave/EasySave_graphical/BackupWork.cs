using System;
using System.Collections.Generic;

namespace EasySave_graphical
{
    public class BackupJob
    {
        private String name;
        private String source;
        private String destination;
        private Boolean isFull;
        private List<string> toBeEncryptedFileExtensions;

        // Getters and setter for each attribute of the backup job
        public string Name { get => name; set => name = value; }
        public string Source { get => source; set => source = value; }
        public string Destination { get => destination; set => destination = value; }
        public Boolean IsFull { get => isFull; set => isFull = value; }
        public List<string> ToBeEncryptedFileExtensions { get => toBeEncryptedFileExtensions; set => toBeEncryptedFileExtensions = value; }

        public BackupJob(String Name, String Source, String Destination, Boolean IsFull, List<string> ToBeEncryptedFileExtensions)
        {
            //TODO:adding check if folder is accessible
            if (Name.Length >= 1 && Source.Length >= 1 && Destination.Length >= 1)
            {
                this.name = Name;
                this.source = Source;
                this.destination = Destination;
                this.isFull = IsFull;
                this.toBeEncryptedFileExtensions = ToBeEncryptedFileExtensions;
            }
            else
            {
                throw new System.ArgumentException("Parameter of type string cannot be empty", "original");
            }

        }


    }
}
