namespace consoleApp
{
    public interface IController
    {
        IView View { get; set; }
        public void UpdateToNextView();
    }
}