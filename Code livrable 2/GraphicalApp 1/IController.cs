﻿namespace consoleApp
{
    // Interface for the controller
    public interface IController
    {
        IView View { get; set; }
        Model Model { get; set; }
    }
}