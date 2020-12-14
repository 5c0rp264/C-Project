using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace EasySave_graphical
{
	public class logManager
	{
		private static logManager instance = null;
        private static readonly Mutex logFileMutex = new Mutex();

        private logManager()
        {

        }

        private static logManager logManagerInstance
        {
			get
            {
				instance = instance ?? new logManager();
                return instance;
			}
		}



        internal static logManager getInstance() => logManagerInstance;


        public void writeLogFile(String toBeWritten)
        {
            logFileMutex.WaitOne();
            // This will just open and write with the indentation appropriated in the state file
            List<Log> loglist = new List<Log>();
            if (!File.Exists(Model.pathToLogFile))
            {
                // Create a file to write to.
                FileStream stream = File.Create(Model.pathToLogFile);
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    loglist.Add(new Log(toBeWritten, (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds));
                    sw.WriteLine(JsonConvert.SerializeObject(loglist, Formatting.Indented));
                    sw.Close();
                }
            }
            else
            {
                string json = File.ReadAllText(Model.pathToLogFile);
                FileStream stream = File.Create(Model.pathToLogFile);
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
    }
}
