using System;

namespace consoleApp
{
    class LanguageView : IView
    {
        private string userInput;

        private IController controller;

        public LanguageView()
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

                // If there is at least one then he has to choose which one he wants to edit
                Console.WriteLine("1. English");
                Console.WriteLine("2. French");

                while (isUserInputValid != true)
                {
                    userInput = Console.ReadLine();
                    isUserInputValid = CheckIfIDInputIsValid(userInput);
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
                if (int.Parse(userInput) == 1)
                {
                    stringIsValid = true;
                    Model.consoleLanguage = "english";
                }else if(int.Parse(userInput) == 2)
                {
                    stringIsValid = true;
                    Model.consoleLanguage = "french";
                }
                else
                {
                    if (Model.consoleLanguage == "english")
                    {
                        Console.WriteLine("\nInvalid response.Try again\n");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("\nRéponse invalide, veuillez réessayer\n");
                        return false;
                    }
                }

                return stringIsValid;
            }
            catch (Exception)
            {
                if (Model.consoleLanguage == "english")
                {
                    Console.WriteLine("\nInvalid response.Try again\n");
                    return false;
                }
                else
                {
                    Console.WriteLine("\nRéponse invalide, veuillez réessayer\n");
                    return false;
                }
            }

        }
    }
}
