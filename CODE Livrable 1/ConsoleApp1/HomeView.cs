using System;
using System.Collections.Generic;
using System.Text;

namespace consoleApp
{
    class HomeView : IView
    {
        private string userInput;

        private string upperCaseUserInput;

        private IController controller;

        public HomeView()
        {
            UserInput = "";
            UpperCaseUserInput = "";
        }

        public string UserInput
        {
            get { return userInput; }
            set { userInput = value; }
        }

        public Controller Controller
        {
            get { return Controller; }
            set { Controller = value; }
        }

        public string UpperCaseUserInput
        {
            get { return upperCaseUserInput; }
            set { upperCaseUserInput = value; }
        }


        public void Show()
        {
            bool isUserInputValid = false;
            Console.WriteLine("");
            Console.WriteLine("[1] Add a backup work");
            Console.WriteLine("[2] Edit a backup work");
            Console.WriteLine("[3] Delete a backup work");
            Console.WriteLine("");
            Console.WriteLine("[0] Execute a backup work");
            Console.WriteLine("");
            Console.WriteLine("What do you want to do :");




            while (isUserInputValid != true)
            {
                userInput = Console.ReadLine();
                isUserInputValid = CheckIfUserInputIsValid(userInput);
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
                if (int.Parse(userInput) <= 3 && int.Parse(userInput) >= 0)
                {
                    stringIsValid = true;
                    switch (int.Parse(userInput))
                    {
                        case 0:
                            controller.View = new ExecuteBackupView();
                            break;
                        case 1:
                            controller.View = new AddView();
                            break;
                        case 2:
                            controller.View = new EditView();
                            break;
                        case 3:
                            controller.View = new DeleteView();
                            break;
                        default:
                            break;
                    }
                    controller.UpdateToNextView();
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
