using System;
using System.Collections.Generic;
using System.Text;

namespace consoleApp
{
    class ExecuteBackupView : IView
    {
        private string userInput;


        private IController controller;

        public ExecuteBackupView()
        {
            UserInput = "";
        }

        public string UserInput
        {
            get { return userInput; }
            set { userInput = value; }
        }

        public IController Controller
        {
            get { return controller; }
            set { controller = value; }
        }


        public void Show()
        {

            Console.WriteLine("[Id]     Name");

            for (int i = 0; i < this.Controller.Model.BackupWorkList.Count; i++) 
            {
                Console.WriteLine("["+(i+1)+"]     "+this.Controller.Model.BackupWorkList[i].Name);
            }
            Console.WriteLine("\nId of backup work you want to execute :");


            bool isUserInputValid = false;
            while (isUserInputValid != true)
            {
                userInput = Console.ReadLine();
                isUserInputValid = CheckIfUserInputIsValid(userInput);
            }

            this.Controller.Model.ExecuteBackupWork(int.Parse(userInput) - 1);

            Console.WriteLine("Done.");
            this.Controller.View = new HomeView();
        }

        //Link the view to the controller
        public void SetController(IController cont)
        {
            Controller = cont;
        }


        private bool CheckIfUserInputIsValid(string userInput)
        {
            try
            {
                bool stringIsValid = false;
                if (int.Parse(userInput) > 0 && int.Parse(userInput) <= this.Controller.Model.BackupWorkList.Count)
                {
                    stringIsValid = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid response.Try again\n");
                }

                return stringIsValid;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
