using System;
using System.Collections.Generic;
using System.Text;

namespace consoleApp
{
    public interface IView
    {
        void Show();
        void SetController(IController cont);
    }
}
