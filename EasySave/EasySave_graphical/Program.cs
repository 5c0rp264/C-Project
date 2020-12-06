using System;
using System.Windows.Forms;

namespace EasySave_graphical
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Model model = new Model();
            graphical_interface view = new graphical_interface();
            Controller controller = new Controller(model, view);
            view.SetController(controller);
            view.reloadListView();
            Application.Run(view);

        }
    }
}
