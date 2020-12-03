using System;

namespace consoleApp
{
    class Controller : IController
    {
        // Our attributes to define the MVC pattern
        private Model model;
        private IView view;

        // Getter and setter
        public IView View { get => view; set => view = value; }
        public Model Model { get => model; set => model = value; }

        public Boolean end = false;

        public Controller()
        {
            // We change the design of the console
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Title = "EasySave from ProSoft";

            //The model and the view can be instantiate in the controller, or in the main program(see graphicalApp)
            model = new Model();
            this.View = new HomeView();

            //Linking the controller to the view, so the view is able to notice the controller when the user gives a valid input
            while (!end)
            {
                this.View.SetController(this);
                this.View.Show();
            }

        }
    }
}
