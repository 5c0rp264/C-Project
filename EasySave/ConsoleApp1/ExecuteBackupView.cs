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
            // If there is no backup job we just return to the homepage
            if (this.Controller.Model.BackupJobList.Count == 0)
            {
                this.Controller.View = new HomeView();
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("You don't have any backup job to execute...\nPress a key to continue");
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas de travail de sauvegarde à exécuter...\nAppuyer sur une touche pour continuer");
                }
                Console.ReadLine();
            }
            else
            {
                // If there is any then the user selects which one to execute
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
                    Console.WriteLine("\nId of backup job you want to execute :");
                }
                else
                {
                    Console.WriteLine("\nId du travail de sauvegarde à exécuter :");
                }

                List<int> idBUJ = new List<int>();
                bool isUserInputValid = false;
                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfIDInputIsValid(userInput);
                    if (isUserInputValid)
                    {
                        // He has the ability to add a backup job to execute at the same time
                        idBUJ.Add(int.Parse(userInput) - 1);
                        if (Model.consoleLanguage == "english")
                        {
                            Console.WriteLine("Do you want to add other backup job [0] No [1] Yes :");
                        }
                        else
                        {
                            Console.WriteLine("Souhaitez-vous ajouter un autre travail de sauvegarde [0] Non [1] Oui :");
                        }
                        userInput = Console.ReadLine();
                        while (userInput != "0" && userInput != "1")
                        {
                            if (Model.consoleLanguage == "english")
                            {
                                Console.WriteLine("Reminder [0] No [1] Yes : :");
                            }
                            else
                            {
                                Console.WriteLine("Rappel [0] Non [1] Oui : :");
                            }
                            userInput = Console.ReadLine();
                        }
                        if (userInput == "1")
                        {
                            isUserInputValid = false;
                            if (Model.consoleLanguage == "english")
                            {
                                Console.WriteLine("\nId of backup job you want to execute :");
                            }
                            else
                            {
                                Console.WriteLine("\nId du travail de sauvegarde à exécuter :");
                            }
                        }
                    }
                }
                List<String> dirFullForDiff = new List<String>();
                for (int i = 0; i < idBUJ.Count; i++)
                {
                    if (!this.Controller.Model.BackupJobList[idBUJ[i]].IsFull)
                    {
                        // If there is a differential backup we ask on which full the users wants to base it's differential backup
                        if (Model.consoleLanguage == "english")
                        {
                            Console.WriteLine("Full backup of reference for diff backup [" + idBUJ[i] + "] " + this.Controller.Model.BackupJobList[idBUJ[i]].Name + " :");
                        }
                        else
                        {
                            Console.WriteLine("Sauvegarde complète de référence pour la différentielle [" + idBUJ[i] + "] " + this.Controller.Model.BackupJobList[idBUJ[i]].Name + " :");
                        }
                        userInput = Console.ReadLine();
                        while (!(userInput.Length >= 1))
                        {
                            if (Model.consoleLanguage == "english")
                            {
                                Console.WriteLine("Please :");
                            }
                            else
                            {
                                Console.WriteLine("S'il vous plaît :");
                            }
                            userInput = Console.ReadLine();
                        }
                        dirFullForDiff.Add(userInput);
                    }
                }

                // In the meantime we add an animation of loading (in another thread so it doesn't stop the backup) 
                Spinner.Start();
                try
                {
                    // Start the backup
                    this.Controller.Model.executeBUJList(idBUJ, dirFullForDiff);
                }
                catch (Exception e)
                {
                    // Return an error message if anything is going wrong
                    Console.WriteLine(e.Message);
                }
                // Stop the spinner because it ended
                Spinner.Stop();

                // We tell the user that everything has been done and we redirect him to he home page
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("\nDone.");
                    Console.WriteLine("Press a key to continue");
                }
                else
                {
                    Console.WriteLine("\nTerminer.");
                    Console.WriteLine("Appuyer sur une touche pour continuer");
                }
                this.Controller.View = new HomeView();
                
                Console.ReadLine();
            }
        }

        //Link the view to the controller
        public void SetController(IController cont)
        {
            Controller = cont;
        }

        // As always we verify that the input is coherent with what we want
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
                return false;
            }
            
        }
    }
}
