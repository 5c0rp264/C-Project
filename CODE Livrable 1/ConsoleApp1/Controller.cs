﻿using System;
using System.Collections.Generic;
using System.Text;

namespace consoleApp
{
    class Controller : IController
    {
        private Model model;
        private View view;

        public Controller()
        {
            //The model and the view can be instantiate in the controller, or in the main program(see graphicalApp)
            model = new Model();
            view = new View();

            //Linking the controller to the view, so the view is able to notice the controller when the user gives a valid input
            view.SetController(this);

            view.GetUserInput();
        }

        //Function called when the user gives a valid input. Start the conversion to the upperCase
        public void UpdateUserInput()
        {
            //Set the value of userInput in the model class, using the input given by the user
            model.UserInput = view.UserInput;

            //Start the conversion and store the result in the view
            view.UpperCaseUserInput = model.ConvertToUpperCase();

            //Display the result in the console
            view.ShowConvertedInput();
        }
    }
}
