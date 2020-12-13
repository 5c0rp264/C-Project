using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace EasySave_graphical
{
    public class Model
    {

        //process watching to stop execution if openned :
        public string processNameToWatch = "Calculator";
        private Thread watchThread;
        private Thread stopWatchThread;
        private static EventWaitHandle waitHandle = new ManualResetEvent(initialState: true);
        private bool wasPaused = false;
        private Controller controller;


        private List<BackupJob> backupJobList;

        // Path to the json files that store the backup job list in the root folder
        private String pathToJsonDB = @"./db.json";
        private String pathToSettings = @"./settings.txt";
        private String pathToStateFile = @"./state.log";
        private String pathToLogFile = @"./logs/" + DateTime.Now.ToString("MM.dd.yyyy") + ".log";

        // Thread for each backup will be stored in this list
        List<Thread> backupThreadList = new List<Thread>();

        // Pause
        public List<String> backupToPause = new List<string>();

        // Settings
        public int maxFileSize = 0;

        public List<BackupJob> BackupJobList
        {
            get { return backupJobList; }
            set { backupJobList = value; }
        }

        public void setController(Controller controlelr)
        {
            this.controller = controlelr;
        }

        public Model()
        {
            //Creating log dir if doesn't exist
            Directory.CreateDirectory(@"./logs/");


            // Simple verification to check if the json file is already present or not
            if (!File.Exists(pathToJsonDB))
            {
                BackupJobList = new List<BackupJob>();
            }
            else
            {
                string json = File.ReadAllText(pathToJsonDB);
                //Console.WriteLine(json);
                BackupJobList = JsonConvert.DeserializeObject<List<BackupJob>>(json);

            }

            // Simple verification to check if the settings file is already present or not
            if (!File.Exists(pathToSettings))
            {
                File.Create("settings.txt");
            }
            else
            {
                string fileSize = "";
                using (StreamReader reader = new StreamReader(pathToSettings))
                {
                    fileSize = reader.ReadLine() ?? "";
                }
                try
                {
                    maxFileSize = Int32.Parse(fileSize);
                }
                catch
                {
                    maxFileSize = 0;
                }
            }
        }


        public Boolean createBackupJob(BackupJob backupJob)
        {
            //Console.WriteLine(File.Exists(pathToJsonDB) ? "File exists." : "File does not exist.");
            if (!File.Exists(pathToJsonDB))
            {
                // We create a file and add the json string in and indexed way
                FileStream stream = File.Create(pathToJsonDB);
                TextWriter tw = new StreamWriter(stream);
                BackupJobList.Add(backupJob);
                String stringjson = JsonConvert.SerializeObject(BackupJobList, Formatting.Indented);
                tw.WriteLine(stringjson);
                tw.Close();
                return true;
            }
            else
            {
                // The file already exists so we had another backup job
                string json = File.ReadAllText(pathToJsonDB);
                //Console.WriteLine(json);
                BackupJobList = JsonConvert.DeserializeObject<List<BackupJob>>(json);
                FileStream stream = File.Create(pathToJsonDB);
                TextWriter tw = new StreamWriter(stream);
                // We add the job
                BackupJobList.Add(backupJob);
                String stringjson = JsonConvert.SerializeObject(BackupJobList, Formatting.Indented);
                tw.WriteLine(stringjson);
                tw.Close();
                return true;

            }
        }

        public void executeBUJList(List<int> backupJobIDList, List<String> fullBackupListForDiff)
        {
            watchThread = new Thread(startWatchingProcess);
            watchThread.IsBackground = true;
            watchThread.Start();

            // The user can select one or multiple backup job to execute in the same time
            int numOfDiff = 0;
            List<BackupJobState> BUJStateList = new List<BackupJobState>();
            for (int i = 0; i < backupJobIDList.Count; i++)
            {
                // For each of them we will save them in our state file
                try
                {
                    BUJStateList.Add(new BackupJobState(this.BackupJobList[backupJobIDList[i]].Name, this.BackupJobList[backupJobIDList[i]].Source, this.BackupJobList[backupJobIDList[i]].Destination, (this.BackupJobList[backupJobIDList[i]].IsFull ? "/Full" : "/Diff") + "-" + backupJobIDList[i] + "-" + DateTime.Now.ToString("MM.dd.yyyy THH.mm.ss.fff"), (this.BackupJobList[backupJobIDList[i]].IsFull ? concernedFile(this.BackupJobList[backupJobIDList[i]].Source).Count : concernedFileDiff(this.BackupJobList[backupJobIDList[i]].Source, fullBackupListForDiff[numOfDiff]).Count), (this.BackupJobList[backupJobIDList[i]].IsFull ? concernedFile(this.BackupJobList[backupJobIDList[i]].Source) : concernedFileDiff(this.BackupJobList[backupJobIDList[i]].Source, fullBackupListForDiff[numOfDiff])), this.BackupJobList[backupJobIDList[i]].IsFull, false, this.BackupJobList[backupJobIDList[i]].ToBeEncryptedFileExtensions));
                }
                catch (Exception e)
                {
                    //Console.WriteLine("BUG")
                    writeLogFile(" /!\\/!\\/!\\ Error for the backup job [" + backupJobIDList[i] + "] " + this.BackupJobList[backupJobIDList[i]].Name);
                    writeLogFile(" /!\\/!\\/!\\ Source : \\\\?\\" + this.BackupJobList[backupJobIDList[i]].Source.Replace(":", "$"));
                    writeLogFile(" /!\\/!\\/!\\ Destination : \\\\?\\" + this.BackupJobList[backupJobIDList[i]].Destination.Replace(":", "$"));
                    writeLogFile(" /!\\/!\\/!\\ Size : unavailable for failed jobs");
                    writeLogFile(" /!\\/!\\/!\\ Transfer time : -1ms");

                    throw e;
                }
                if (!this.BackupJobList[backupJobIDList[i]].IsFull)
                {
                    numOfDiff++;
                }
            }
            writeStateFile(BUJStateList);
            numOfDiff = 0;
            // For each backup job the user wants to execute we will execute it based on it's type (full / differential)
            for (int i = 0; i < backupJobIDList.Count; i++)
            {
                this.controller.View.addProgressBar(i);
                BUJStateList[i].ISACtive = true;
                // Update the state file
                writeStateFile(BUJStateList);
                writeLogFile("Starting the backup job [" + backupJobIDList[i] + "] " + BUJStateList[i].Name);
                writeLogFile("Source : \\?\\" + BUJStateList[i].Source.Replace(":", "$"));
                writeLogFile("Destination : \\\\?\\" + BUJStateList[i].Destination.Replace(":", "$"));
                writeLogFile("Source directory size : " + CalculateFolderSize(BUJStateList[i].Source));
                writeLogFile("Destination directory size : " + BUJStateList[i].TotalSizeOfElligbleFiles);
                if (this.BackupJobList[backupJobIDList[i]].IsFull)
                {
                    // Full copy
                    Thread fullThread = new Thread(() =>
                    {
                        DirectoryCopy(this.BackupJobList[backupJobIDList[i]].Source, BUJStateList[i].Destination + BUJStateList[i].FolderName, i, BUJStateList);
                    });
                    fullThread.Name = "FULL_THREAD_ID_" + backupJobIDList[i];
                    fullThread.Start();
                    backupThreadList.Add(fullThread);
                    this.controller.View.UpdateBackupPauseList(BackupJobList[backupJobIDList[i]].Name);
                }
                else
                {
                    // Differential copy
                    int current_backupIndex = numOfDiff;
                    Thread diffThread = new Thread(() => DirectoryDifferentialCopy(this.BackupJobList[backupJobIDList[i]].Source, BUJStateList[i].Destination + BUJStateList[i].FolderName, fullBackupListForDiff[current_backupIndex], i, BUJStateList));
                    diffThread.Name = "DIFF_THREAD_ID_" + backupJobIDList[i];
                    diffThread.Start();
                    backupThreadList.Add(diffThread);
                    // The number is increasing before the thread has start
                    numOfDiff++;
                    this.controller.View.UpdateBackupPauseList(BackupJobList[backupJobIDList[i]].Name);
                }
                // At the end the work at the index i is no more active
                BUJStateList[i].ISACtive = false;
                // We reupdate it one last time
                writeStateFile(BUJStateList);
                writeLogFile("Transfer time : " + BUJStateList[i].Stopwatch.Elapsed);

            }
            stopWatchThread = new Thread(stopWatchingProcess);
            stopWatchThread.IsBackground = true;
            stopWatchThread.Start();
        }

        public void abortThread()
        {
            foreach (Thread item in backupThreadList)
            {
                try
                {
                    item.Abort(); // Stop each thread one by one
                }
                catch (ThreadAbortException abortException)
                {
                    Thread.ResetAbort(); // Prevent app from closing
                }
            }
        }

        public void suspendThread()
        {
            // Should pause the threads
            waitHandle.Reset();
        }

        public void resumeThread()
        {
            // Should resume the threads
            waitHandle.Set();
        }

        public void editBackupJob(int idToEdit, String name, String source, String destination, Boolean isFull, List<string> extToCrypt)
        {
            // When the user edits a work he can change or let the ancient name of each criteria, in any way we update them here
            this.BackupJobList[idToEdit].Name = name;
            this.BackupJobList[idToEdit].Source = source;
            this.BackupJobList[idToEdit].Destination = destination;
            this.BackupJobList[idToEdit].IsFull = isFull;
            this.BackupJobList[idToEdit].ToBeEncryptedFileExtensions = extToCrypt;
            saveBUJ();
        }


        public void deleteBackupJob(int idToEdit)
        {
            // Really easy method to delete a backup job
            this.BackupJobList.RemoveAt(idToEdit);
            saveBUJ();
        }







        // --------------------- Method to make life easier ---------------------------------
        private static Mutex stateFileMutex = new Mutex();
        private void writeStateFile(List<BackupJobState> BUJSList)
        {
            stateFileMutex.WaitOne();
            // This will just open and write with the indentation appropriated in the state file
            FileStream stream = File.Create(pathToStateFile);
            TextWriter tw = new StreamWriter(stream);
            try
            {
                String stringjson = JsonConvert.SerializeObject(BUJSList, Formatting.Indented);
                tw.WriteLine(stringjson);
            }
            catch (Exception exc)
            {
                Debug.Print(exc.ToString());
            }

            tw.Close();
            stateFileMutex.ReleaseMutex();
        }


        private static Mutex logFileMutex = new Mutex();
        private void writeLogFile(String toBeWritten)
        {
            logFileMutex.WaitOne();
            // This will just open and write with the indentation appropriated in the state file
            List<Log> loglist = new List<Log>();
            if (!File.Exists(pathToLogFile))
            {
                // Create a file to write to.
                FileStream stream = File.Create(pathToLogFile);
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    loglist.Add(new Log(toBeWritten, (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds));
                    sw.WriteLine(JsonConvert.SerializeObject(loglist, Formatting.Indented));
                    sw.Close();
                }
            }
            else
            {
                string json = File.ReadAllText(pathToLogFile);
                FileStream stream = File.Create(pathToLogFile);
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    loglist = JsonConvert.DeserializeObject<List<Log>>(json);
                    if (loglist != null)
                    {
                        loglist.Add(new Log(toBeWritten, (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds));
                    }
                    sw.WriteLine(JsonConvert.SerializeObject(loglist, Formatting.Indented));
                    sw.Close();
                }
            }

            logFileMutex.ReleaseMutex();
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
                _ = waitHandle.WaitOne();
            }

            // If copying subdirectories, copy them and their contents to new location.

            foreach (DirectoryInfo subdir in dirs)
            {
                totalCount.AddRange(concernedFile(subdir.FullName));
                _ = waitHandle.WaitOne();
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


            if (!dirsrc.Exists)
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
                _ = waitHandle.WaitOne();
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                totalCount.AddRange(concernedFileDiff(subdir.FullName, Path.Combine(comparisonDirName, subdir.Name)));
                _ = waitHandle.WaitOne();
            }
            return totalCount;

        }

        // Number of file beyond the limit set by the user
        private static Mutex copyBigFile = new Mutex();

        private void DirectoryCopy(string sourceDirName, string destDirName, int index, List<BackupJobState> BUJS)
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
                // If the file is too big and there is already another file being copied
                if (file.Length > maxFileSize && maxFileSize != 0)
                {
                    // We wait for the semaphore to accept us
                    copyBigFile.WaitOne();
                    // Progress bar update
                    this.controller.View.updateTracking(index, Int32.Parse(Math.Ceiling((BUJS[index].Progress) * 100).ToString()), BUJS[index].Name);

                    Boolean didCryptIt = false;
                    foreach (string ext in BUJS[index].ToBeEncryptedFileExtensions)
                    {
                        if (ext == file.Extension && ext != "")
                        {
                            ProcessStartInfo psi = new ProcessStartInfo("CryptoSoft.exe");
                            psi.WorkingDirectory = "../../../../CryptoSoft/CryptoSoft.scorp264/CryptoSoft/bin/Debug/netcoreapp3.1/";
                            psi.Arguments = "\"" + Path.Combine(sourceDirName, file.Name) + "\" \"" + Path.Combine(destDirName, file.Name) + "\"";
                            Process proc = Process.Start(psi);
                            proc.WaitForExit();
                            writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was: " + proc.ExitCode + "ms");
                            didCryptIt = true;
                            //Console.WriteLine(Path.Combine(destDirName, file.Name));
                        }

                    }
                    if (!didCryptIt)
                    {
                        file.CopyTo(Path.Combine(destDirName, file.Name), false);
                        writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was: 0ms (no encryption)");
                    }
                    BUJS[index].FilesTransfered.Add(new myOwnFileInfo(file.Length, file.FullName));
                    //Console.Write(BUJS[index].FilesTransfered.Count / BUJS[index].TotalElligibleFile);
                    BUJS[index].Progress = ((float)BUJS[index].FilesTransfered.Count) / ((float)BUJS[index].TotalElligibleFile);
                    BUJS[index].SizeOfRemainingFiles = BUJS[index].TotalSizeOfElligbleFiles - BUJS[index].FilesTransfered.Sum(item => item.fileSize);
                    writeStateFile(BUJS);
                    
                    // Check if we need to suspend the thread or not
                    foreach (var item in backupToPause)
                    {
                        if (BUJS[index].Name == item)
                        {
                            _ = waitHandle.WaitOne();
                        }
                    }

                    // We release the semaphore because we don't need one anymore
                    copyBigFile.ReleaseMutex();
                }
                else
                {
                    // copy the file -> there is no problem with the size
                    // Progress bar update
                    if (BUJS[index].TotalElligibleFile == 1)
                    {
                        this.controller.View.updateTracking(index, Int32.Parse(Math.Ceiling((BUJS[index].Progress) * 100).ToString()), BUJS[index].Name, "end");
                    }
                    else
                    {
                        this.controller.View.updateTracking(index, Int32.Parse(Math.Ceiling((BUJS[index].Progress) * 100).ToString()), BUJS[index].Name);
                    }

                    Boolean didCryptIt = false;
                    foreach (string ext in BUJS[index].ToBeEncryptedFileExtensions)
                    {
                        if (ext == file.Extension && ext != "")
                        {
                            ProcessStartInfo psi = new ProcessStartInfo("CryptoSoft.exe");
                            psi.WorkingDirectory = "../../../../CryptoSoft/CryptoSoft.scorp264/CryptoSoft/bin/Debug/netcoreapp3.1/";
                            psi.Arguments = "\"" + Path.Combine(sourceDirName, file.Name) + "\" \"" + Path.Combine(destDirName, file.Name) + "\"";
                            Process proc = Process.Start(psi);
                            proc.WaitForExit();
                            writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was: " + proc.ExitCode + "ms");
                            didCryptIt = true;
                            //Console.WriteLine(Path.Combine(destDirName, file.Name));
                        }

                    }
                    if (!didCryptIt)
                    {
                        file.CopyTo(Path.Combine(destDirName, file.Name), false);
                        writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was: 0ms (no encryption )");
                    }
                    BUJS[index].FilesTransfered.Add(new myOwnFileInfo(file.Length, file.FullName));
                    //Console.Write(BUJS[index].FilesTransfered.Count / BUJS[index].TotalElligibleFile);
                    BUJS[index].Progress = ((float)BUJS[index].FilesTransfered.Count) / ((float)BUJS[index].TotalElligibleFile);
                    BUJS[index].SizeOfRemainingFiles = BUJS[index].TotalSizeOfElligbleFiles - BUJS[index].FilesTransfered.Sum(item => item.fileSize);
                    writeStateFile(BUJS);

                    // Do we need to stop it or not
                    foreach (var item in backupToPause)
                    {
                        if (BUJS[index].Name == item)
                        {
                            _ = waitHandle.WaitOne();
                        }
                    }
                }

            }

            // If copying subdirectories, copy them and their contents to new location.

            foreach (DirectoryInfo subdir in dirs)
            {
                DirectoryCopy(subdir.FullName, Path.Combine(destDirName, subdir.Name), index, BUJS);
                // Do we need to stop it or not
                foreach (var item in backupToPause)
                {
                    if (BUJS[index].Name == item)
                    {
                        _ = waitHandle.WaitOne();
                    }
                }
            }
        }
        private void DirectoryDifferentialCopy(string sourceDirName, string destDirName, string comparisonDirName, int index, List<BackupJobState> BUJS)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dirsrc = new DirectoryInfo(sourceDirName);
            DirectoryInfo dircomp = new DirectoryInfo(comparisonDirName);


            if (!dirsrc.Exists)
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
                if (file.Length > maxFileSize && maxFileSize != 0)
                {
                    // We wait for the mutex to accept us
                    copyBigFile.WaitOne();
                    this.controller.View.updateTracking(index, Int32.Parse(Math.Ceiling((BUJS[index].Progress) * 100).ToString()), BUJS[index].Name);
                    if (!File.Exists(Path.Combine(comparisonDirName, file.Name)))
                    {
                        Boolean didCryptIt = false;
                        foreach (string ext in BUJS[index].ToBeEncryptedFileExtensions)
                        {
                            if (ext == file.Extension)
                            {
                                ProcessStartInfo psi = new ProcessStartInfo("CryptoSoft.exe");
                                psi.WorkingDirectory = "../../../../CryptoSoft/CryptoSoft.scorp264/CryptoSoft/bin/Debug/netcoreapp3.1/";
                                psi.Arguments = "\"" + Path.Combine(sourceDirName, file.Name) + "\" \"" + Path.Combine(destDirName, file.Name) + "\"";
                                Process proc = Process.Start(psi);
                                proc.WaitForExit();
                                writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was: " + proc.ExitCode + "ms");
                                didCryptIt = true;
                            }

                        }
                        if (!didCryptIt)
                        {
                            file.CopyTo(Path.Combine(destDirName, file.Name), false);
                            writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was: 0ms ( no encryption )");
                        }
                        BUJS[index].FilesTransfered.Add(new myOwnFileInfo(file.Length, file.FullName));
                        BUJS[index].Progress = ((float)BUJS[index].FilesTransfered.Count) / ((float)BUJS[index].TotalElligibleFile);
                        BUJS[index].SizeOfRemainingFiles = BUJS[index].TotalSizeOfElligbleFiles - BUJS[index].FilesTransfered.Sum(item => item.fileSize);
                        writeStateFile(BUJS);
                        foreach (var item in backupToPause)
                        {
                            if (BUJS[index].Name == item)
                            {
                                _ = waitHandle.WaitOne();
                            }
                        }
                    }
                    else if (File.Exists(Path.Combine(comparisonDirName, file.Name)))
                    {
                        if (CalculateMD5(Path.Combine(comparisonDirName, file.Name)) != CalculateMD5(Path.Combine(sourceDirName, file.Name)))
                        {
                            Boolean didCryptIt = false;
                            foreach (string ext in BUJS[index].ToBeEncryptedFileExtensions)
                            {
                                if (ext == file.Extension)
                                {
                                    ProcessStartInfo psi = new ProcessStartInfo("CryptoSoft.exe");
                                    psi.WorkingDirectory = "../../../../CryptoSoft/CryptoSoft.scorp264/CryptoSoft/bin/Debug/netcoreapp3.1/";
                                    psi.Arguments = "\"" + Path.Combine(sourceDirName, file.Name) + "\" \"" + Path.Combine(destDirName, file.Name) + "\"";
                                    Process proc = Process.Start(psi);
                                    proc.WaitForExit();
                                    writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was : " + proc.ExitCode + "ms");
                                    didCryptIt = true;
                                }
                            }
                            if (!didCryptIt)
                            {
                                file.CopyTo(Path.Combine(destDirName, file.Name), false);
                                writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was: 0ms ( no encryption )");
                            }
                            BUJS[index].FilesTransfered.Add(new myOwnFileInfo(file.Length, file.FullName));
                            BUJS[index].Progress = ((float)BUJS[index].FilesTransfered.Count) / ((float)BUJS[index].TotalElligibleFile);
                            BUJS[index].SizeOfRemainingFiles = BUJS[index].TotalSizeOfElligbleFiles - BUJS[index].FilesTransfered.Sum(item => item.fileSize);
                            writeStateFile(BUJS);
                            foreach (var item in backupToPause)
                            {
                                if (BUJS[index].Name == item)
                                {
                                    _ = waitHandle.WaitOne();
                                }
                            }
                        }
                    }
                    // We release the mutex because we don't need one anymore
                    copyBigFile.ReleaseMutex();
                }
                else
                {
                    this.controller.View.updateTracking(index, Int32.Parse(Math.Ceiling((BUJS[index].Progress) * 100).ToString()), BUJS[index].Name);
                    if (!File.Exists(Path.Combine(comparisonDirName, file.Name)))
                    {
                        Boolean didCryptIt = false;
                        foreach (string ext in BUJS[index].ToBeEncryptedFileExtensions)
                        {
                            if (ext == file.Extension)
                            {
                                ProcessStartInfo psi = new ProcessStartInfo("CryptoSoft.exe");
                                psi.WorkingDirectory = "../../../../CryptoSoft/CryptoSoft.scorp264/CryptoSoft/bin/Debug/netcoreapp3.1/";
                                psi.Arguments = "\"" + Path.Combine(sourceDirName, file.Name) + "\" \"" + Path.Combine(destDirName, file.Name) + "\"";
                                Process proc = Process.Start(psi);
                                proc.WaitForExit();
                                writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was: " + proc.ExitCode + "ms");
                                didCryptIt = true;
                            }

                        }
                        if (!didCryptIt)
                        {
                            file.CopyTo(Path.Combine(destDirName, file.Name), false);
                            writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was: 0ms ( no encryption )");
                        }
                        BUJS[index].FilesTransfered.Add(new myOwnFileInfo(file.Length, file.FullName));
                        BUJS[index].Progress = ((float)BUJS[index].FilesTransfered.Count) / ((float)BUJS[index].TotalElligibleFile);
                        BUJS[index].SizeOfRemainingFiles = BUJS[index].TotalSizeOfElligbleFiles - BUJS[index].FilesTransfered.Sum(item => item.fileSize);
                        writeStateFile(BUJS);
                        foreach (var item in backupToPause)
                        {
                            if (BUJS[index].Name == item)
                            {
                                _ = waitHandle.WaitOne();
                            }
                        }
                    }
                    else if (File.Exists(Path.Combine(comparisonDirName, file.Name)))
                    {
                        if (CalculateMD5(Path.Combine(comparisonDirName, file.Name)) != CalculateMD5(Path.Combine(sourceDirName, file.Name)))
                        {
                            Boolean didCryptIt = false;
                            foreach (string ext in BUJS[index].ToBeEncryptedFileExtensions)
                            {
                                if (ext == file.Extension)
                                {
                                    ProcessStartInfo psi = new ProcessStartInfo("CryptoSoft.exe");
                                    psi.WorkingDirectory = "../../../../CryptoSoft/CryptoSoft.scorp264/CryptoSoft/bin/Debug/netcoreapp3.1/";
                                    psi.Arguments = "\"" + Path.Combine(sourceDirName, file.Name) + "\" \"" + Path.Combine(destDirName, file.Name) + "\"";
                                    Process proc = Process.Start(psi);
                                    proc.WaitForExit();
                                    writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was : " + proc.ExitCode + "ms");
                                    didCryptIt = true;
                                }
                            }
                            if (!didCryptIt)
                            {
                                file.CopyTo(Path.Combine(destDirName, file.Name), false);
                                writeLogFile("Encryption time for " + Path.Combine(destDirName, file.Name) + " was: 0ms ( no encryption )");
                            }
                            BUJS[index].FilesTransfered.Add(new myOwnFileInfo(file.Length, file.FullName));
                            BUJS[index].Progress = ((float)BUJS[index].FilesTransfered.Count) / ((float)BUJS[index].TotalElligibleFile);
                            BUJS[index].SizeOfRemainingFiles = BUJS[index].TotalSizeOfElligbleFiles - BUJS[index].FilesTransfered.Sum(item => item.fileSize);
                            writeStateFile(BUJS);
                            foreach (var item in backupToPause)
                            {
                                if (BUJS[index].Name == item)
                                {
                                    _ = waitHandle.WaitOne();
                                }
                            }
                        }
                    }
                }

            }

            foreach (DirectoryInfo subdir in dirs)
            {
                DirectoryDifferentialCopy(subdir.FullName, Path.Combine(destDirName, subdir.Name), Path.Combine(comparisonDirName, subdir.Name), index, BUJS);
                foreach (var item in backupToPause)
                {
                    if (BUJS[index].Name == item)
                    {
                        _ = waitHandle.WaitOne();
                    }
                }
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

        //This allow us to write folder size in log file
        protected static float CalculateFolderSize(string folder)
        {
            float folderSize = 0.0f;
            try
            {
                //Checks if the path is valid or not
                if (!Directory.Exists(folder))
                    return folderSize;
                else
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles(folder))
                        {
                            if (File.Exists(file))
                            {
                                FileInfo finfo = new FileInfo(file);
                                folderSize += finfo.Length;
                            }
                        }

                        foreach (string dir in Directory.GetDirectories(folder))
                            folderSize += CalculateFolderSize(dir);
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
            }
            return folderSize;
        }

        // Our backup job are in the db.json file so we put it in this file
        private void saveBUJ()
        {
            FileStream stream = File.Create(pathToJsonDB);
            TextWriter tw = new StreamWriter(stream);
            String stringjson = JsonConvert.SerializeObject(BackupJobList, Formatting.Indented);
            tw.WriteLine(stringjson);
            tw.Close();
        }


        private void startWatchingProcess()
        {
            while (true)
            {
                Process[] processes = Process.GetProcessesByName(processNameToWatch);
                if (processes.Length >= 1 && !wasPaused)
                {
                    waitHandle.Reset();
                    Debug.Print("CALCULATOR !!!!!!");
                    this.wasPaused = true;
                }
                else if (processes.Length == 0 && wasPaused)
                {
                    waitHandle.Set();
                    Console.WriteLine("restarted");
                    this.wasPaused = false;
                }
                Thread.Sleep(250);
            }
        }

        private void stopWatchingProcess()
        {
            int threadsAlive;

            do
            {
                threadsAlive = 0;
                foreach (Thread item in backupThreadList)
                {
                    if (item.IsAlive)
                    {
                        threadsAlive++;
                    }
                }
                Thread.Sleep(250);
            } while (threadsAlive > 0);
            this.controller.View.removeProgressBar();
            this.controller.View.clearBackupPauseListDelegate();
            watchThread.Abort();
        }

        public void openLogFile()
        {
            ProcessStartInfo newLogFile = new ProcessStartInfo(DateTime.Now.ToString("MM.dd.yyyy") + ".log");
            newLogFile.WorkingDirectory = "logs";

            if (!IsFileLocked("logs/" + DateTime.Now.ToString("MM.dd.yyyy") + ".log"))
            {
                Process.Start(newLogFile);
            }
            else
            {
                Console.WriteLine("In use"); //TODO: tell the user that file is already in use
            }
        }

        public void openStateFile()
        {
            if (!IsFileLocked("state.log"))
            {
                Process.Start("state.log");
            }
        }

        private bool IsFileLocked(string filename)
        {
            bool Locked = false;
            try
            {
                FileStream fs =
                    File.Open(filename, FileMode.OpenOrCreate,
                    FileAccess.ReadWrite, FileShare.None);
                fs.Close();
            }
            catch (IOException ex)
            {
                Locked = true;
            }
            return Locked;
        }
        public void WriteToFileInFirstLine(string Path, string Text)
        {
            string content = File.ReadAllText(Path);
            content = Text + "\n" + content;
            File.WriteAllText(Path, content);
        }
    }


}
