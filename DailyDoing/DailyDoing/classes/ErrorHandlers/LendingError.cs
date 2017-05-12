using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyDoing.classes.ErrorHandlers
{
    class LendingError : ErrorHandler
    {
        public LendingError()
        {
            this.errorHeader = getErrorHeader();
            this.errorDescription = getErrorDescription();
        }

        private string getErrorDescription()
        {
            return "Title and the StartDate are mandatory fields, please give some Information.";
        }

        private string getErrorHeader()
        {
            return "Information Missing!";
        }
    }
}
