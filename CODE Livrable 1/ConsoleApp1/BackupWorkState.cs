using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace consoleApp
{
    public class myOwnFileInfo {
        public long fileSize;
        public String fullName;

        public myOwnFileInfo(long fileSize, string fullName)
        {
            this.fileSize = fileSize;
            this.fullName = fullName;
        }
    }
    public class BackupWorkState
    {
        private String name;
        private String source;
        private String destination;
        private String folderName;
        private int totalElligibleFile;
        private List<myOwnFileInfo> elligibleFiles;
        private long totalSizeOfElligbleFiles;
        private Boolean isFull;
        private Boolean isActive;
        private List<myOwnFileInfo> filesTransfered;
        private long sizeOfRemainingFiles;
        private float progress;
        private Stopwatch stopwatch;


        public String Name { get => name; set => name = value; }
        public String Source { get => source; set => source = value; }
        public String Destination { get => destination; set => destination = value; }
        public String FolderName { get => folderName; set => folderName = value; }
        public int TotalElligibleFile { get => totalElligibleFile; set => totalElligibleFile = value; }
        public List<myOwnFileInfo> ElligibleFiles { get => elligibleFiles; set => elligibleFiles = value; }
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
        public List<myOwnFileInfo> FilesTransfered { get => filesTransfered; set => filesTransfered = value;}

        public Stopwatch Stopwatch { get => stopwatch; set => stopwatch = value; }
        public float Progress { get => progress; set => progress = value; }
        public long TotalSizeOfElligbleFiles { get => totalSizeOfElligbleFiles; set => totalSizeOfElligbleFiles = value; }
        public long SizeOfRemainingFiles { get => sizeOfRemainingFiles; set => sizeOfRemainingFiles = value; }

        public BackupWorkState(String Name, String Source, String Destination, String FolderName, int TotalElligibleFile, List<myOwnFileInfo> ElligibleFiles, Boolean IsFull, Boolean IsActive)
        {
            //TODO:adding check if folder is accessible
            if (Name.Length >= 1 && Source.Length >= 1 && Destination.Length >= 1)
            {
                this.name = Name;
                this.source = Source;
                this.destination = Destination;
                this.folderName = FolderName;
                this.elligibleFiles = ElligibleFiles;
                this.totalElligibleFile = TotalElligibleFile;
                this.totalSizeOfElligbleFiles = this.elligibleFiles.Sum(items => items.fileSize);
                this.isFull = IsFull;
                this.isActive = ISACtive;
                this.filesTransfered = new List<myOwnFileInfo>();
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
