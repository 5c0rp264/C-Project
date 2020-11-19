using System;
using System.Collections.Generic;
using System.Text;

namespace consoleApp
{
    class Model
    {
        private string userInput;


        public string UserInput
        {
            get { return userInput; }
            set { userInput = value; }
        }

        public Model()
        {
            UserInput ="";
        }

        //The function used to convert the input to the upperCase.
        public string ConvertToUpperCase()
        {
            string userInUpperCase = UserInput.ToUpper();

            return userInUpperCase;
        }

    }
}
