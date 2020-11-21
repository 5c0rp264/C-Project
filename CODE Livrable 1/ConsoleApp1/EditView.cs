using System;
using System.Collections.Generic;
using System.Text;

namespace consoleApp
{
    class EditView : IView
    {
        private string userInput;

        private string upperCaseUserInput;

        private IController controller;

        public EditView()
        {
            UserInput = "";
            UpperCaseUserInput = "";
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

        public string UpperCaseUserInput
        {
            get { return upperCaseUserInput; }
            set { upperCaseUserInput = value; }
        }


        public void Show()
        {
            bool isUserInputValid = false;
            Console.WriteLine("[Id]     Name");

            for (int i = 0; i < this.Controller.Model.BackupWorkList.Count; i++)
            {
                Console.WriteLine("[" + (i + 1) + "]     " + this.Controller.Model.BackupWorkList[i].Name);
            }
            Console.WriteLine("Id of backup work you want to edit :");



            while (isUserInputValid != true)
            {
                userInput = Console.ReadLine();
                isUserInputValid = CheckIfIDInputIsValid(userInput);
            }

            int idToEdit = int.Parse(userInput)-1;

            String name = this.Controller.Model.BackupWorkList[idToEdit].Name;
            String source = this.Controller.Model.BackupWorkList[idToEdit].Source;
            String destination = this.Controller.Model.BackupWorkList[idToEdit].Destination;
            Boolean isFull = this.Controller.Model.BackupWorkList[idToEdit].IsFull;

            Console.WriteLine("Name [" + this.Controller.Model.BackupWorkList[idToEdit].Name + "] :");
            userInput = Console.ReadLine();

            if (userInput.Length >= 1){
                name = userInput;
            }
            Console.WriteLine("source [" + this.Controller.Model.BackupWorkList[idToEdit].Source + "] :");
            userInput = Console.ReadLine();

            if (userInput.Length >= 1)
            {
                source = userInput;
            }
            Console.WriteLine("Destination [" + this.Controller.Model.BackupWorkList[idToEdit].Destination + "] :");
            userInput = Console.ReadLine();

            if (userInput.Length >= 1)
            {
                destination = userInput;
            }
            Console.WriteLine("Do you want [0]Diff or [1]Full [" + this.Controller.Model.BackupWorkList[idToEdit].Name + "] :");
            userInput = Console.ReadLine();

            while (isUserInputValid != true)
            {
                userInput = Console.ReadLine();
                if (userInput == "0" || userInput == "1")
                {
                    if (userInput == "1")
                    {
                        isFull = true;
                    }
                    else
                    {
                        isFull = false;
                    }
                    isUserInputValid = true;
                }
                else
                {
                    Console.WriteLine("Thanks to enter a valid value.");
                }
            }
            try
            {
                this.Controller.Model.editBackupWork(idToEdit, name, source, destination, isFull);
            }
            catch
            {
                Console.WriteLine("Unable to edit this backup work.");
            }
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
