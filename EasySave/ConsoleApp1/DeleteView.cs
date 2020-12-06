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
            if (this.Controller.Model.BackupJobList.Count == 0)
            {
                this.Controller.View = new HomeView();
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("You don't have any backup job to delete...\nPress a key to continue");
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas de travail de sauvegarde à supprimer...\nAppuyer sur une touche pour continuer");
                }
                Console.ReadLine();
            }
            else
            {
                // If there is at least one then he has to choose which one he wants to edit
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("[Id]     Name");
                }
                else
                {
                    Console.WriteLine("[Id]     Nom");
                }

                for (int i = 0; i < this.Controller.Model.BackupJobList.Count; i++)
                {
                    Console.WriteLine("[" + (i + 1) + "]     " + this.Controller.Model.BackupJobList[i].Name);
                }
                
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("ID of backup job you want to delete :");
                }
                else
                {
                    Console.WriteLine("ID du travail de sauvegarde à supprimer :");
                }

                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfIDInputIsValid(userInput);
                }

                int idToDelete = int.Parse(userInput) - 1;

                try
                {
                    this.Controller.Model.deleteBackupJob(idToDelete);
                }
                catch
                {
                    if (Model.consoleLanguage == "english")
                    {
                        Console.WriteLine("Press a key to continue");
                    }
                    else
                    {
                        Console.WriteLine("Appuyer sur une touche pour continuer");
                    }
                }
                this.Controller.View = new HomeView();
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("Press a key to continue");
                }
                else
                {
                    Console.WriteLine("Appuyer sur une touche pour continuer");
                }
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
                if (int.Parse(userInput) > 0 && int.Parse(userInput) <= this.Controller.Model.BackupJobList.Count)
                {
                    stringIsValid = true;
                }
                else
                {
                    if (Model.consoleLanguage == "english")
                    {
                        Console.WriteLine("\nInvalid response.Try again\n");
                    }
                    else
                    {
                        Console.WriteLine("\nRéponse invalide. Veuillez réessayer\n");
                    }
                }

                return stringIsValid;
            }
            catch (Exception)
            {
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("\nInvalid response.Try again\n");
                }
                else
                {
                    Console.WriteLine("\nRéponse invalide. Veuillez réessayer\n");
                }
                return false;
            }

        }
    }
}
