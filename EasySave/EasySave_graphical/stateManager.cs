using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace EasySave_graphical
{
    public class stateManager
    {
        private static stateManager instance = null;
        private static readonly Mutex stateFileMutex = new Mutex();

        private stateManager()
        {
        }

        private static stateManager stateManagerInstance
        {
            get
            {
                instance = instance ?? new stateManager();
                return instance;
            }
        }

        internal static stateManager getInstance() => stateManagerInstance;



        public void writeStateFile(List<BackupJobState> BUJSList)
        {
            stateFileMutex.WaitOne();
            // This will just open and write with the indentation appropriated in the state file
            FileStream stream = File.Create(Model.pathToStateFile);
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
    }
}
