using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace consoleApp
{
    public class Model
    {
        private List<BackupWork> backupWorkList;

        private String pathToJsonDB = @"./db.json";
        public List<BackupWork> BackupWorkList
        {
            get { return backupWorkList; }
            set { backupWorkList = value; }
        }

        public Model()
        {
            
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
                FileStream stream = File.Create(pathToJsonDB);
                TextWriter tw = new StreamWriter(stream);
                BackupWorkList.Add(backupWork);
                String stringjson = JsonConvert.SerializeObject(BackupWorkList);
                tw.WriteLine(stringjson);
                tw.Close();
                return true;
            }
            else
            {
                string json = File.ReadAllText(pathToJsonDB);
                //Console.WriteLine(json);
                BackupWorkList = JsonConvert.DeserializeObject<List<BackupWork>>(json);
                FileStream stream = File.Create(pathToJsonDB);
                TextWriter tw = new StreamWriter(stream);
                if (BackupWorkList.Count >= 5)
                {
                    return false;
                }
                else
                {
                    BackupWorkList.Add(backupWork);
                    String stringjson = JsonConvert.SerializeObject(BackupWorkList);
                    tw.WriteLine(stringjson);
                    tw.Close();
                    return true;
                }
                
            }
        }

        public void ExecuteBackupWork(int backupWorkID)
        {
            DirectoryCopy(this.BackupWorkList[backupWorkID].Source, this.BackupWorkList[backupWorkID].Destination + "/" + DateTime.Now.ToString("Full MM.dd.yyyy THH.mm.ss.fff"));
        }

        public void ExecuteDifferentialBackupWork(int backupWorkID, String fullBUDir)
        {
            DirectoryDifferentialCopy(this.BackupWorkList[backupWorkID].Source, this.BackupWorkList[backupWorkID].Destination + "/" + DateTime.Now.ToString("Full MM.dd.yyyy THH.mm.ss.fff"), fullBUDir);
        }




        private static void DirectoryCopy(string sourceDirName, string destDirName)
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
            }

            // If copying subdirectories, copy them and their contents to new location.

            foreach (DirectoryInfo subdir in dirs)
            {
                DirectoryCopy(subdir.FullName, Path.Combine(destDirName, subdir.Name));   
            }
            
        }
        private static void DirectoryDifferentialCopy(string sourceDirName, string destDirName, string comparisonDirName)
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
                }
                else if (File.Exists(Path.Combine(comparisonDirName, file.Name))){
                    if (CalculateMD5(Path.Combine(comparisonDirName, file.Name)) != CalculateMD5(Path.Combine(sourceDirName, file.Name)))
                    {
                        file.CopyTo(Path.Combine(destDirName, file.Name), false);
                    }
                }
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                DirectoryDifferentialCopy(subdir.FullName, Path.Combine(destDirName, subdir.Name), Path.Combine(comparisonDirName, subdir.Name));
            }

        }


        static string CalculateMD5(string filename)
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

    }
}
