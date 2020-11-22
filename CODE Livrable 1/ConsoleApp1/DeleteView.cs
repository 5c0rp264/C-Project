using System;
using System.Collections.Generic;
using System.Text;

namespace consoleApp
{
    class DeleteView : IView
    {
        private string userInput;

        private IController controller;

        public DeleteView()
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
            bool isUserInputValid = false;
            Console.WriteLine("[Id]     Name");

            for (int i = 0; i < this.Controller.Model.BackupWorkList.Count; i++)
            {
                Console.WriteLine("[" + (i + 1) + "]     " + this.Controller.Model.BackupWorkList[i].Name);
            }
            Console.WriteLine("Id of backup work you want to delete :");

            while (isUserInputValid != true)
            {
                userInput = Console.ReadLine();
                isUserInputValid = CheckIfIDInputIsValid(userInput);
            }

            int idToDelete = int.Parse(userInput) - 1;

            try
            {
                this.Controller.Model.deleteBackupWork(idToDelete);
            }
            catch {
                Console.WriteLine("Unable to delete this backup work.");
            }
            this.Controller.View = new HomeView();
        }

        //Link the view to the controller
        public void SetController(IController cont)
        {
            controller = cont;
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
                Console.WriteLine("\nInvalid response.Try again\n");
                return false;
            }

        }
    }
}
