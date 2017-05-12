using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDoing.classes.ErrorHandlers
{
    class ContactError: ErrorHandler
    {
        public ContactError()
        {
            this.errorHeader = getErrorHeader();
            this.errorDescription = getErrorDescription();
        }
        private string getErrorDescription()
        {
            return "Name and Firstname are mandatory fields, please give some Information.";
        }

        private string getErrorHeader()
        {
            return "Information Missing!";
        }
    }
}
