using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyDoing.classes
{
    class ErrorHandler
    {
        protected string errorDescription = String.Empty;
        protected string errorHeader = String.Empty;
        public ErrorHandler()
        {

        }
        public void showErrorBox()
        {
            MessageBox.Show(errorDescription, errorHeader, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
