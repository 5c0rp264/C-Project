using System;
using System.Collections.Generic;
using System.Text;

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


            try
            {
                BackupWork backupWork = new BackupWork(name, source, destination);
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("\nUnable to generate this backup work.\n");
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
                return false;
            }
            
        }
    }
}
