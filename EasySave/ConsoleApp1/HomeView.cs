using System;

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

            if (Model.consoleLanguage == "english")
            {
                // Clear what is currently displayed in the console and print the menu
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("     ███████╗ █████╗ ███████╗██╗   ██╗    ███████╗ █████╗ ██╗   ██╗███████╗");
                Console.WriteLine("     ██╔════╝██╔══██╗██╔════╝╚██╗ ██╔╝    ██╔════╝██╔══██╗██║   ██║██╔════╝");
                Console.WriteLine("     █████╗  ███████║███████╗ ╚████╔╝     ███████╗███████║██║   ██║█████╗");
                Console.WriteLine("     ██╔══╝  ██╔══██║╚════██║  ╚██╔╝      ╚════██║██╔══██║╚██╗ ██╔╝██╔══╝");
                Console.WriteLine("     ███████╗██║  ██║███████║   ██║       ███████║██║  ██║ ╚████╔╝ ███████╗");
                Console.WriteLine("     ╚══════╝╚═╝  ╚═╝╚══════╝   ╚═╝       ╚══════╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝");
                Console.WriteLine("\n\nWhat would you like to start with ?");
                Console.WriteLine("");
                Console.WriteLine("[0] Execute backup jobs");
                Console.WriteLine("");
                Console.WriteLine("[1] Add a backup job");
                Console.WriteLine("[2] Edit a backup job");
                Console.WriteLine("[3] Delete a backup job");
                Console.WriteLine("[4] Change Language");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Your choice :");
            }
            else
            {
                // Clear what is currently displayed in the console and print the menu
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("     ███████╗ █████╗ ███████╗██╗   ██╗    ███████╗ █████╗ ██╗   ██╗███████╗");
                Console.WriteLine("     ██╔════╝██╔══██╗██╔════╝╚██╗ ██╔╝    ██╔════╝██╔══██╗██║   ██║██╔════╝");
                Console.WriteLine("     █████╗  ███████║███████╗ ╚████╔╝     ███████╗███████║██║   ██║█████╗");
                Console.WriteLine("     ██╔══╝  ██╔══██║╚════██║  ╚██╔╝      ╚════██║██╔══██║╚██╗ ██╔╝██╔══╝");
                Console.WriteLine("     ███████╗██║  ██║███████║   ██║       ███████║██║  ██║ ╚████╔╝ ███████╗");
                Console.WriteLine("     ╚══════╝╚═╝  ╚═╝╚══════╝   ╚═╝       ╚══════╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝");
                Console.WriteLine("\n\nPar quoi souhaitez-vous commencer ?");
                Console.WriteLine("");
                Console.WriteLine("[0] Exécuter un travail de sauvegarde");
                Console.WriteLine("");
                Console.WriteLine("[1] Ajouter un travail de sauvegarde");
                Console.WriteLine("[2] Éditer un travail de sauvegarde");
                Console.WriteLine("[3] Supprimer un travail de sauvegarde");
                Console.WriteLine("[4] Changer de langue");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Votre choix :");
            }





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
                if (int.Parse(userInput) <= 4 && int.Parse(userInput) >= 0)
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
                        case 4:
                            controller.View = new LanguageView();
                            break;
                        default:
                            break;
                    }
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
    }
}
