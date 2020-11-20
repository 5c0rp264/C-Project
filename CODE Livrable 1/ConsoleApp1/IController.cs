namespace consoleApp
{
    public interface IController
    {
        IView View { get; set; }
        Model Model { get; set; }
        public void UpdateToNextView();
    }
}