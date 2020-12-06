using System;
using System.Windows.Forms;

namespace EasySave_graphical
{
    public class Controller //: IController
    {
        // Our attributes to define the MVC pattern
        private Model model;
        private graphical_interface view;

        // Getter and setter
        public graphical_interface View { get => view; set => view = value; }
        public Model Model { get => model; set => model = value; }

        public Boolean end = false;

        public Controller(Model model, graphical_interface view)
        {
            // We change the design of the console

            //The model and the view can be instantiate in the controller, or in the main program(see graphicalApp)
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            this.View = view ?? throw new ArgumentNullException(nameof(view));
        }
    }
}
