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
            //TODO: do while instead of while
            if (this.Controller.Model.BackupWorkList.Count >= 5)
            {
                this.Controller.View = new HomeView();
                Console.WriteLine("It seems that you already have the maximum amount of backup work authorized...\nPress a key to continue");
                Console.ReadLine();
            }
            else
            {
                bool isUserInputValid = false;
                Console.WriteLine("Name of backup work to create :");
                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfUserInputIsValid(userInput);
                }
                String name = userInput;

                isUserInputValid = false;
                Console.WriteLine("Source of backup work to create :");
                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfUserInputIsValid(userInput);
                }
                String source = userInput;

                isUserInputValid = false;
                Console.WriteLine("Destination of backup work to create :");
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



                try
                {
                    BackupWork backupWork = new BackupWork(name, source, destination, isAFullBackup);
                    //Console.WriteLine(backupWork);
                    //Console.WriteLine(this.controller);
                    if (this.controller.Model.createBackupWork(backupWork))
                    {
                        Console.WriteLine("Backup work added with success.\n");
                    }
                    else
                    {
                        Console.WriteLine("Unable to create, make sure you don't have already 5 backup works.\n");
                    }
                    this.Controller.View = new HomeView();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("\nUnable to generate this backup work.\n");
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
