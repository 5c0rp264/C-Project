using System;
using System.Collections.Generic;
using System.Text;

namespace consoleApp
{
    class View
    {
        private string userInput;

        private string upperCaseUserInput;

        private IController controller;

        public View()
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
            get { return Controller; }
            set { Controller = value; }
        }

        public string UpperCaseUserInput
        {
            get { return upperCaseUserInput; }
            set { upperCaseUserInput = value; }
        }


        public void GetUserInput()
        {
            bool isUserInputValid = false;

            Console.WriteLine("Veuillez saisir la chaîne de caractères(entre 1 et 8 caractères) à convertir");

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

        public void ShowConvertedInput()
        {
            Console.WriteLine("Le résultat de la conversion est : " + UpperCaseUserInput);
        }



        private bool CheckIfUserInputIsValid(string userInput)
        {
            bool stringIsValid = false;

            if (userInput.Length <= 8 && userInput.Length >= 1)
            {
                stringIsValid = true;
                controller.UpdateUserInput();
            }
            else
            {
                Console.WriteLine("La chaîne de caractères ne respecte pas le format demandé(entre 1 et 8 caractères). Veuillez saisir une chaîne valide");
            }

            return stringIsValid;
        }
    }
}
