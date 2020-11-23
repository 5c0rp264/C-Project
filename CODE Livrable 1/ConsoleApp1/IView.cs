namespace consoleApp
{
    // Interface for the view
    public interface IView
    {
        void Show();
        void SetController(IController cont);
    }
}
