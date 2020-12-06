using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace consoleApp
{
    class AddView : IView
    {
        private string userInput;

        private IController controller;

        public AddView()
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
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("Name of backup job to create :");
                }
                else
                {
                    Console.WriteLine("Nom du travail de sauvegarde à créer :");
                }

                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfUserInputIsValid(userInput);
                }
                String name = userInput;

                isUserInputValid = false;
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("Source of backup job to create :");
                }
                else
                {
                    Console.WriteLine("Source du travail de sauvegarde à créer :");
                }

                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfUserInputIsValid(userInput);
                }
                String source = userInput;

                isUserInputValid = false;
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("Destination of backup job to create :");
                }
                else
                {
                    Console.WriteLine("Destination du travail de sauvegarde à créer :");
                }

            while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfUserInputIsValid(userInput);
                }
                String destination = userInput;

                isUserInputValid = false;
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("What type of backup do you want :\n[0] Differential\n[1] Full");
                }
                else
                {
                    Console.WriteLine("Quel type de travail de sauvegarde souhaitez-vous :\n[0] Différentielle\n[1] Complète");
                }

                Boolean isAFullBackup = false;
                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    if (userInput == "0" || userInput == "1")
                    {
                        if (userInput == "1")
                        {
                            isAFullBackup = true;
                        }
                        else
                        {
                            isAFullBackup = false;
                        }
                        isUserInputValid = true;
                    }
                    else
                    {
                    if (Model.consoleLanguage == "english")
                    {
                        Console.WriteLine("Thanks to enter a valid value");
                    }
                    else
                    {
                        Console.WriteLine("Merci d'entrer une valeur valide");
                    }
                }
                }


            
            if (Model.consoleLanguage == "english")
            {
                Console.WriteLine("Extensions that will be encrypted (comma separated, no input for no encryption):");
            }
            else
            {
                Console.WriteLine("Extensions à crypter (séparées d'une virgule, laissez vide pour ne pas crypter):");
            }
            userInput = Console.ReadLine();
            List<string> extToCrypt = parseUserInputAsList(userInput);


            // We save the backup job (or tell the user that we couldn't)
            try
                {
                    BackupJob backupJob = new BackupJob(name, source, destination, isAFullBackup, extToCrypt);
                    if (this.controller.Model.createBackupJob(backupJob))
                    {
                        
                    if (Model.consoleLanguage == "english")
                    {
                        Console.WriteLine("backup job added with success.\n");
                    }
                    else
                    {
                        Console.WriteLine("Travail de sauvegarde ajouté avec succès\n");
                    }
                }
                    else
                    {
                    if (Model.consoleLanguage == "english")
                    {
                        Console.WriteLine("An error occured, try again later\n");
                    }
                    else
                    {
                        Console.WriteLine("Une erreur est survenue, veuillez réessayer\n");
                    }

                    }
                    this.Controller.View = new HomeView();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("\nUnable to generate this backup job.\n");
                    Console.WriteLine("Press a key to continue");
                }
                else
                {
                    Console.WriteLine("\nImpossible de générer ce travail de sauvegarde.\n");
                    Console.WriteLine("Appuyer sur une touche pour continuer");
                }

                }
                Console.ReadLine();
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("Press a key to continue");
                }
                else
                {
                    Console.WriteLine("Appuyer sur une touche pour continuer");
                }
        }

        //Link the view to the controller
        public void SetController(IController cont)
        {
            controller = cont;
        }


        private bool CheckIfUserInputIsValid(string userInput)
        {
            try
            {
                bool stringIsValid = false;
                if (userInput.Length >= 1)
                {
                    stringIsValid = true;
                    //Environment.Exit(0);
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



        private List<string> parseUserInputAsList(string userInput)
        {
            //List <string> listParsed = new List<string>();
            List<string> listParsed = new List<string>(Regex.Replace(userInput, @"\s+", "").Split(","));
            return listParsed;
        }
    }
}
