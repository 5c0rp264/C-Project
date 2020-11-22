using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace consoleApp
{
    public class BackupWorkState
    {
        private String name;
        private String source;
        private String destination;
        private String folderName;
        private int totalElligibleFile;
        private Boolean isFull;
        private Boolean isActive;
        private List<FileInfo> filesTransfered;
        private Stopwatch stopwatch;


        public String Name { get => name; set => name = value; }
        public String Source { get => source; set => source = value; }
        public String Destination { get => destination; set => destination = value; }
        public String FolderName { get => folderName; set => folderName = value; }
        public int TotalElligibleFile { get => totalElligibleFile; set => totalElligibleFile = value; }
        public Boolean IsFull { get => isFull; set => isFull = value; }
        public Boolean ISACtive { get => isActive;
            set {
                isActive = value;
                if (value)
                {
                    this.stopwatch.Start();
                }else
                {
                    this.stopwatch.Stop();
                }
            }
        }
        public List<FileInfo> FilesTransfered { get => filesTransfered; set => filesTransfered = value; }


        public BackupWorkState(String Name, String Source, String Destination, String FolderName, int TotalElligibleFile, Boolean IsFull, Boolean IsActive)
        {
            //TODO:adding check if folder is accessible
            if (Name.Length >= 1 && Source.Length >= 1 && Destination.Length >= 1)
            {
                this.name = Name;
                this.source = Source;
                this.destination = Destination;
                this.folderName = FolderName;
                this.totalElligibleFile = TotalElligibleFile;
                this.isFull = IsFull;
                this.isActive = ISACtive;
                this.filesTransfered = new List<FileInfo>();
                this.stopwatch = new Stopwatch();
                stopwatch.Reset();
            }
            else
            {
                throw new System.ArgumentException("Parameter cannot be empty", "original");
            }

        }


    }
}
