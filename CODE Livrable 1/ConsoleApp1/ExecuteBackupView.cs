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


            List<int> idBUW = new List<int>();
            bool isUserInputValid = false;
            while (isUserInputValid != true)
            {
                userInput = Console.ReadLine();
                isUserInputValid = CheckIfIDInputIsValid(userInput);
                if (isUserInputValid) {
                    idBUW.Add(int.Parse(userInput) - 1);
                    Console.WriteLine("Do you want to add other backup job [0]No [Yes] :");
                    userInput = Console.ReadLine();
                    while (userInput != "0" && userInput != "1")
                    {
                        Console.WriteLine("Reminder [0]No [Yes] :");
                        userInput = Console.ReadLine();
                    }
                    if (userInput == "1")
                    {
                        isUserInputValid = false;
                        Console.WriteLine("\nId of backup work you want to execute :");
                    }
                }
            }
            List<String> dirFullForDiff = new List<String>();
            for (int i = 0; i < idBUW.Count; i++)
            {
                if (!this.Controller.Model.BackupWorkList[idBUW[i]].IsFull)
                {
                    Console.WriteLine("Full backup of reference for diff backup ["+ idBUW[i] + "] " + this.Controller.Model.BackupWorkList[idBUW[i]].Name + " :");
                    userInput = Console.ReadLine();
                    while (userInput.Length >= 1)
                    {
                        Console.WriteLine("lease :");
                        userInput = Console.ReadLine();
                    }
                    dirFullForDiff.Add(userInput);
                }
            }

            
            try
            {
                this.Controller.Model.executeBUJList(idBUW, dirFullForDiff);
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

            Console.WriteLine("Done.");
            this.Controller.View = new HomeView();
        }

        //Link the view to the controller
        public void SetController(IController cont)
        {
            Controller = cont;
        }


        private bool CheckIfIDInputIsValid(string userInput)
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
