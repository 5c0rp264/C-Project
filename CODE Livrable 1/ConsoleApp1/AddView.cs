using System;

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
            if (this.Controller.Model.BackupJobList.Count >= 5)
            {
                // If there are already 5 backup job we prevent from adding one by redirecting the the homepage
                this.Controller.View = new HomeView();
                Console.WriteLine("It seems that you already have the maximum amount of backup job authorized...\nPress a key to continue");
                Console.ReadLine();
            }
            else
            {
                // Else we ask for details about the new backup job
                bool isUserInputValid = false;
                Console.WriteLine("Name of backup job to create :");
                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfUserInputIsValid(userInput);
                }
                String name = userInput;

                isUserInputValid = false;
                Console.WriteLine("Source of backup job to create :");
                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfUserInputIsValid(userInput);
                }
                String source = userInput;

                isUserInputValid = false;
                Console.WriteLine("Destination of backup job to create :");
                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfUserInputIsValid(userInput);
                }
                String destination = userInput;

                isUserInputValid = false;
                Console.WriteLine("What type of backup do you want :\n[0] Differential\n[1]Full");
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
                        Console.WriteLine("Thanks to enter a valid value.");
                    }
                }


                // We save the backup job (or tell the user that we couldn't)
                try
                {
                    BackupJob backupJob = new BackupJob(name, source, destination, isAFullBackup);
                    if (this.controller.Model.createBackupJob(backupJob))
                    {
                        Console.WriteLine("backup job added with success.\n");
                    }
                    else
                    {
                        Console.WriteLine("Unable to create, make sure you don't have already 5 backup jobs.\n");
                    }
                    this.Controller.View = new HomeView();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("\nUnable to generate this backup job.\n");
                }

                Console.WriteLine("Press a key to continue");
                Console.ReadLine();
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
