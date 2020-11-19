using System;
using System.Collections.Generic;
using System.Text;

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

            Console.WriteLine("Id of backup work you want to edit :");



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
                if (userInput.Length >= 1)
                {
                    stringIsValid = true;
                    Environment.Exit(0);
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
