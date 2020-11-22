namespace consoleApp
{
    public interface IView
    {
        void Show();
        void SetController(IController cont);
    }
}
