using System;
using System.Collections.Generic;
using System.IO;
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
            BackupWorkList = new List<BackupWork>();
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

    }
}
