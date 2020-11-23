using System;

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
            // If there is no backup job => Redirect the user to home page
            bool isUserInputValid = false;
            if (this.Controller.Model.BackupWorkList.Count == 0)
            {
                this.Controller.View = new HomeView();
                Console.WriteLine("You don't have any backup job to delete...\nPress a key to continue");
                Console.ReadLine();
            }
            else
            {
                // If there is at least one then he has to choose which one he wants to edit
                Console.WriteLine("[Id]     Name");

                for (int i = 0; i < this.Controller.Model.BackupWorkList.Count; i++)
                {
                    Console.WriteLine("[" + (i + 1) + "]     " + this.Controller.Model.BackupWorkList[i].Name);
                }
                Console.WriteLine("Id of backup job you want to delete :");

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
                catch
                {
                    Console.WriteLine("Unable to delete this backup job.");
                }
                this.Controller.View = new HomeView();
                Console.WriteLine("Press a key to continue");
                Console.ReadLine();
            }
        }

        //Link the view to the controller
        public void SetController(IController cont)
        {
            controller = cont;
        }

        // As in edit mode we just certify that anything entered is valid thanks to this method
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
