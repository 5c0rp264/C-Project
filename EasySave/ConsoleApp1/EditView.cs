﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            // A simple check if the user really can use this method or not (if he has no backup job to edit we redirect him to he home page)
            if (this.Controller.Model.BackupJobList.Count == 0)
            {
                this.Controller.View = new HomeView();
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("You don't have any backup job to edit...\nPress a key to continue");
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas de travail de sauvegarde à éditer...\nAppuyer sur une touche pour continuer");
                }
                Console.ReadLine();
            }
            else
            {
                // If he has any backup job we ask him which one he wants to edit
                Console.WriteLine("[Id]     Name");

                for (int i = 0; i < this.Controller.Model.BackupJobList.Count; i++)
                {
                    Console.WriteLine("[" + (i + 1) + "]     " + this.Controller.Model.BackupJobList[i].Name);
                }
                
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("Id of backup job you want to edit :");
                }
                else
                {
                    Console.WriteLine("Id du travail de sauvegarde à éditer :");
                }


                while (isUserInputValid != true)
                {
                    // Wait for a valid input
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfIDInputIsValid(userInput);
                }

                int idToEdit = int.Parse(userInput) - 1;

                String name = this.Controller.Model.BackupJobList[idToEdit].Name;
                String source = this.Controller.Model.BackupJobList[idToEdit].Source;
                String destination = this.Controller.Model.BackupJobList[idToEdit].Destination;
                Boolean isFull = this.Controller.Model.BackupJobList[idToEdit].IsFull;

                // From line 75 to 118 we just want him to edit a backup job criteria by criteria with good inputs
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("Name [" + this.Controller.Model.BackupJobList[idToEdit].Name + "] :");
                }
                else
                {
                    Console.WriteLine("Nom [" + this.Controller.Model.BackupJobList[idToEdit].Name + "] :");
                }
                
                userInput = Console.ReadLine();

                if (userInput.Length >= 1)
                {
                    name = userInput;
                }
                Console.WriteLine("Source [" + this.Controller.Model.BackupJobList[idToEdit].Source + "] :");
                userInput = Console.ReadLine();

                if (userInput.Length >= 1)
                {
                    source = userInput;
                }
                Console.WriteLine("Destination [" + this.Controller.Model.BackupJobList[idToEdit].Destination + "] :");
                userInput = Console.ReadLine();

                if (userInput.Length >= 1)
                {
                    destination = userInput;
                }

                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("Do you want [0]Diff or [1]Full [" + (this.Controller.Model.BackupJobList[idToEdit].IsFull ? "1" : "0") + "] :");
                }
                else
                {
                    Console.WriteLine("Quel type voulez-vous [0] Différentielle or [1] Complète [" + (this.Controller.Model.BackupJobList[idToEdit].IsFull ? "1" : "0") + "] :");
                }
                
                isUserInputValid = false;
                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    if (userInput == "0" || userInput == "1" || userInput == "")
                    {
                        if (userInput == "1")
                        {
                            isFull = true;
                        }
                        else if (userInput == "0")
                        {
                            isFull = false;
                        }
                        isUserInputValid = true;
                    }
                    else
                    {
                        if (Model.consoleLanguage == "english")
                        {
                            Console.WriteLine("Thanks to enter a valid value.");
                        }
                        else
                        {
                            Console.WriteLine("Merci d'entrer une valeur valide.");
                        }
                    }
                }

                List<string> extToCrypt = this.Controller.Model.BackupJobList[idToEdit].ToBeEncryptedFileExtensions;
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("Extensions that will be encrypted (comma separated, no input for no encryption) [" + string.Join(", ", this.Controller.Model.BackupJobList[idToEdit].ToBeEncryptedFileExtensions) + "]:");
                }
                else
                {
                    Console.WriteLine("Extensions à crypter (séparées d'une virgule, laissez vide pour ne pas crypter) [" + string.Join(", ", this.Controller.Model.BackupJobList[idToEdit].ToBeEncryptedFileExtensions) + "]:");
                }
                userInput = Console.ReadLine();
                if (userInput.Length > 1)
                {
                    extToCrypt = parseUserInputAsList(userInput);
                }
                try
                {
                    this.Controller.Model.editBackupJob(idToEdit, name, source, destination, isFull, extToCrypt);
                }
                catch
                {
                    if (Model.consoleLanguage == "english")
                    {
                        Console.WriteLine("Unable to edit this backup job.");
                        Console.WriteLine("Press a key to continue");
                    }
                    else
                    {
                        Console.WriteLine("Impossible d'éditer ce travail de sauvegarde.");
                        Console.WriteLine("Appuyer sur une touche pour continuer");
                    }

                    
                }
                // Return to the home view
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
            Controller = cont;
        }

        // Certify that the user input is valid
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


        private List<string> parseUserInputAsList(string userInput)
        {
            //List <string> listParsed = new List<string>();
            List<string> listParsed = new List<string>(Regex.Replace(userInput, @"\s+", "").Split(","));
            return listParsed;
        }


    }
}
