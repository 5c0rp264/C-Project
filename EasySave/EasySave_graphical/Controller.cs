using System;
using System.Threading;
using System.Windows.Forms;

namespace EasySave_graphical
{
    public class Controller //: IController
    {
        private static Mutex mutex;

        // Our attributes to define the MVC pattern
        private Model model;
        private graphical_interface view;

        // Getter and setter
        public graphical_interface View { get => view; set => view = value; }
        public Model Model { get => model; set => model = value; }

        public Boolean end = false;

        public Controller(Model model, graphical_interface view)
        {
            // We need to make sure that only one instance is running
            const string appName = "EasySave";
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show(appName + " is already running ! The application will now close.", "Instantiation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.ReadKey();
                return;
            }

            //The model and the view can be instantiate in the controller, or in the main program(see graphicalApp)
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            this.View = view ?? throw new ArgumentNullException(nameof(view));
        }
    }
}
