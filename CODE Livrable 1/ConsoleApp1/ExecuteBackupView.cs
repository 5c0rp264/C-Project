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
                isUserInputValid = CheckIfIDInputIsValid(userInput);
            }
            int idBUW = int.Parse(userInput)-1;

            if (this.controller.Model.BackupWorkList[idBUW].IsFull)
            {
                try
                {
                    this.Controller.Model.ExecuteBackupWork(idBUW);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("\nPath of the full backup you want to diff from :");


                isUserInputValid = false;
                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    if (userInput.Length >= 1)
                    {
                        isUserInputValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter it.");
                    }
                }
                try
                {
                    this.Controller.Model.ExecuteDifferentialBackupWork(idBUW, userInput);

                }catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("Done.");
            this.Controller.View = new HomeView();
            Console.WriteLine("Press a key to continue");
            Console.ReadLine();
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
