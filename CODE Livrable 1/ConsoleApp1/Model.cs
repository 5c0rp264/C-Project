using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;


//TODO: Add logs and state file.


namespace consoleApp
{
    public class Model
    {
        private List<BackupWork> backupWorkList;

        // Path to the json files that store the backup work list in the root folder
        private String pathToJsonDB = @"./db.json";
        private String pathToStateFile = @"./state.log";
        public List<BackupWork> BackupWorkList
        {
            get { return backupWorkList; }
            set { backupWorkList = value; }
        }

        public Model()
        {
            // Simple verification to check if the json file is already present or not
            if (!File.Exists(pathToJsonDB))
            {
                BackupWorkList = new List<BackupWork>();
            }
            else
            {
                string json = File.ReadAllText(pathToJsonDB);
                //Console.WriteLine(json);
                BackupWorkList = JsonConvert.DeserializeObject<List<BackupWork>>(json);

            }
        }


        public Boolean createBackupWork(BackupWork backupWork)
        {
            //Console.WriteLine(File.Exists(pathToJsonDB) ? "File exists." : "File does not exist.");
            if (!File.Exists(pathToJsonDB))
            {   
                // We create a file and add the json string in and indexed way
                FileStream stream = File.Create(pathToJsonDB);
                TextWriter tw = new StreamWriter(stream);
                BackupWorkList.Add(backupWork);
                String stringjson = JsonConvert.SerializeObject(BackupWorkList, Formatting.Indented);
                tw.WriteLine(stringjson);
                tw.Close();
                return true;
            }
            else
            {
                // The file already exists so we had another backup work
                string json = File.ReadAllText(pathToJsonDB);
                //Console.WriteLine(json);
                BackupWorkList = JsonConvert.DeserializeObject<List<BackupWork>>(json);
                FileStream stream = File.Create(pathToJsonDB);
                TextWriter tw = new StreamWriter(stream);
                if (BackupWorkList.Count >= 5)
                {
                    // Only if the number is not already 5
                    tw.Close();
                    return false;
                }
                else
                {
                    // If we don't have 5 backup file we don't add it
                    BackupWorkList.Add(backupWork);
                    String stringjson = JsonConvert.SerializeObject(BackupWorkList, Formatting.Indented);
                    tw.WriteLine(stringjson);
                    tw.Close();
                    return true;
                }

            }
        }

        public void executeBUJList(List<int> backupWorkIDList, List<String> fullBackupListForDiff)
        {
            // The user can select one or multiple backup work to execute in the same time
            int numOfDiff = 0;
            List<BackupWorkState> BUWStateList = new List<BackupWorkState>();
            for (int i = 0; i < backupWorkIDList.Count; i++)
            {
                // For each of them we will save them in our state file
                BUWStateList.Add(new BackupWorkState(this.BackupWorkList[backupWorkIDList[i]].Name, this.BackupWorkList[backupWorkIDList[i]].Source, this.BackupWorkList[backupWorkIDList[i]].Destination, (this.BackupWorkList[backupWorkIDList[i]].IsFull ? "/Full" : "/Diff") + DateTime.Now.ToString("MM.dd.yyyy THH.mm.ss.fff"), (this.BackupWorkList[backupWorkIDList[i]].IsFull ? concernedFile(this.BackupWorkList[backupWorkIDList[i]].Source).Count : concernedFileDiff(this.BackupWorkList[backupWorkIDList[i]].Source, fullBackupListForDiff[numOfDiff]).Count), (this.BackupWorkList[backupWorkIDList[i]].IsFull ? concernedFile(this.BackupWorkList[backupWorkIDList[i]].Source) : concernedFileDiff(this.BackupWorkList[backupWorkIDList[i]].Source, fullBackupListForDiff[numOfDiff])), this.BackupWorkList[backupWorkIDList[i]].IsFull, false));
                if (!this.BackupWorkList[backupWorkIDList[i]].IsFull)
                {
                    numOfDiff++;
                }
            }
            writeStateFile(BUWStateList);
            numOfDiff = 0;
            // For each backup work the user wants to execute we will execute it based on it's type (full / differential)
            for (int i = 0; i < backupWorkIDList.Count; i++)
            {
                BUWStateList[i].ISACtive = true;
                // Update the state file
                writeStateFile(BUWStateList);
                if (this.BackupWorkList[backupWorkIDList[i]].IsFull)
                {
                    // Full copy
                    DirectoryCopy(this.BackupWorkList[backupWorkIDList[i]].Source, BUWStateList[i].Destination + BUWStateList[i].FolderName, i, BUWStateList);
                }
                else
                {
                    // Differential copy
                    DirectoryDifferentialCopy(this.BackupWorkList[backupWorkIDList[i]].Source, BUWStateList[i].Destination, fullBackupListForDiff[numOfDiff], i, BUWStateList);
                    numOfDiff++;
                }
                // At the end the work at the index i is no more active
                BUWStateList[i].ISACtive = false;
                // We reupdate it one last time
                writeStateFile(BUWStateList);
            }
        }


        public void editBackupWork(int idToEdit, String name, String source, String destination, Boolean isFull)
        {
            // When the user edits a work he can change or let the ancient name of each criteria, in any way we update them here
            this.BackupWorkList[idToEdit].Name = name;
            this.BackupWorkList[idToEdit].Source = source;
            this.BackupWorkList[idToEdit].Destination = destination;
            this.BackupWorkList[idToEdit].IsFull = isFull;
            saveBUW();
        }


        public void deleteBackupWork(int idToEdit)
        {
            // Really easy method to delete a backup work
            this.BackupWorkList.RemoveAt(idToEdit);
            saveBUW();
        }







        // --------------------- Method to make life easier ---------------------------------

        private void writeStateFile(List<BackupWorkState> BUWSList)
        {
            // This will just open and write with the indentation appropriated in the state file
            FileStream stream = File.Create(pathToStateFile);
            TextWriter tw = new StreamWriter(stream);
            String stringjson = JsonConvert.SerializeObject(BUWSList, Formatting.Indented);
            tw.WriteLine(stringjson);
            tw.Close();
        }

        private static List<myOwnFileInfo> concernedFile(string sourceDirName)
        {
            // We count the number of file in our folder if it doesn't exist we return an exception
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            List<myOwnFileInfo> totalCount = new List<myOwnFileInfo>();
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                totalCount.Add(new myOwnFileInfo(file.Length, file.FullName));
            }

            // If copying subdirectories, copy them and their contents to new location.

            foreach (DirectoryInfo subdir in dirs)
            {
                totalCount.AddRange(concernedFile(subdir.FullName));
            }
            return totalCount;
        }


        // Same method than before but for differential backup
        private static List<myOwnFileInfo> concernedFileDiff(string sourceDirName, string comparisonDirName)
        {
            // We count the number of file in our folder if it doesn't exist we return an exception
            List<myOwnFileInfo> totalCount = new List<myOwnFileInfo>();
            DirectoryInfo dirsrc = new DirectoryInfo(sourceDirName);
            DirectoryInfo dircomp = new DirectoryInfo(comparisonDirName);


            if (!dirsrc.Exists || !dircomp.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source or full backup directory does not exist or could not be found: "
                    + sourceDirName + "\nor\n" + comparisonDirName);
            }

            DirectoryInfo[] dirs = dirsrc.GetDirectories();
            //DirectoryInfo[] dirsComp = dirsrc.GetDirectories();

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dirsrc.GetFiles();
            foreach (FileInfo file in files)
            {
                if (!File.Exists(Path.Combine(comparisonDirName, file.Name)))
                {
                    totalCount.Add(new myOwnFileInfo(file.Length, file.FullName));
                }
                else if (File.Exists(Path.Combine(comparisonDirName, file.Name)))
                {
                    if (CalculateMD5(Path.Combine(comparisonDirName, file.Name)) != CalculateMD5(Path.Combine(sourceDirName, file.Name)))
                    {
                        totalCount.Add(new myOwnFileInfo(file.Length, file.FullName));
                    }
                }
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                totalCount.AddRange(concernedFileDiff(subdir.FullName, Path.Combine(comparisonDirName, subdir.Name)));
            }
            return totalCount;

        }


        private void DirectoryCopy(string sourceDirName, string destDirName, int index, List<BackupWorkState> BUWS)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(destDirName, file.Name), false);
                BUWS[index].FilesTransfered.Add(new myOwnFileInfo(file.Length, file.FullName));
                //Console.Write(BUWS[index].FilesTransfered.Count / BUWS[index].TotalElligibleFile);
                BUWS[index].Progress = ((float)BUWS[index].FilesTransfered.Count) / ((float)BUWS[index].TotalElligibleFile);
                BUWS[index].SizeOfRemainingFiles = BUWS[index].TotalSizeOfElligbleFiles - BUWS[index].FilesTransfered.Sum(item => item.fileSize);
                writeStateFile(BUWS);

            }

            // If copying subdirectories, copy them and their contents to new location.

            foreach (DirectoryInfo subdir in dirs)
            {
                DirectoryCopy(subdir.FullName, Path.Combine(destDirName, subdir.Name), index, BUWS);
            }

        }
        private void DirectoryDifferentialCopy(string sourceDirName, string destDirName, string comparisonDirName, int index, List<BackupWorkState> BUWS)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dirsrc = new DirectoryInfo(sourceDirName);
            DirectoryInfo dircomp = new DirectoryInfo(comparisonDirName);


            if (!dirsrc.Exists || !dircomp.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source or full backup directory does not exist or could not be found: "
                    + sourceDirName + "\nor\n" + comparisonDirName);
            }

            DirectoryInfo[] dirs = dirsrc.GetDirectories();
            //DirectoryInfo[] dirsComp = dirsrc.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dirsrc.GetFiles();
            foreach (FileInfo file in files)
            {
                if (!File.Exists(Path.Combine(comparisonDirName, file.Name)))
                {
                    file.CopyTo(Path.Combine(destDirName, file.Name), false);
                    BUWS[index].FilesTransfered.Add(new myOwnFileInfo(file.Length, file.FullName));
                    BUWS[index].Progress = ((float)BUWS[index].FilesTransfered.Count) / ((float)BUWS[index].TotalElligibleFile);
                    BUWS[index].SizeOfRemainingFiles = BUWS[index].TotalSizeOfElligbleFiles - BUWS[index].FilesTransfered.Sum(item => item.fileSize);
                    writeStateFile(BUWS);
                }
                else if (File.Exists(Path.Combine(comparisonDirName, file.Name)))
                {
                    if (CalculateMD5(Path.Combine(comparisonDirName, file.Name)) != CalculateMD5(Path.Combine(sourceDirName, file.Name)))
                    {
                        file.CopyTo(Path.Combine(destDirName, file.Name), false);
                        BUWS[index].FilesTransfered.Add(new myOwnFileInfo(file.Length, file.FullName));
                        BUWS[index].Progress = ((float)BUWS[index].FilesTransfered.Count) / ((float)BUWS[index].TotalElligibleFile);
                        BUWS[index].SizeOfRemainingFiles = BUWS[index].TotalSizeOfElligbleFiles - BUWS[index].FilesTransfered.Sum(item => item.fileSize);
                        writeStateFile(BUWS);
                    }
                }
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                DirectoryDifferentialCopy(subdir.FullName, Path.Combine(destDirName, subdir.Name), Path.Combine(comparisonDirName, subdir.Name), index, BUWS);
            }

        }

        // This will permit the comparison of file based on their md5 signature
        private static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
        
        // Our backup work are in the db.json file so we put it in this file
        private void saveBUW()
        {
            FileStream stream = File.Create(pathToJsonDB);
            TextWriter tw = new StreamWriter(stream);
            String stringjson = JsonConvert.SerializeObject(BackupWorkList, Formatting.Indented);
            tw.WriteLine(stringjson);
            tw.Close();
        }

    }
}
