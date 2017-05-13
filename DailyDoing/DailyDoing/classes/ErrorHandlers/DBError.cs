using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyDoing.classes
{
    class DBError : ErrorHandler
    {
        int errorcode;
        string message;
        public DBError(int errorcode)
        {
            this.errorHeader = getErrorHeader();
            this.errorDescription = getErrorDescription();
            this.errorcode = errorcode;
        }

        private string getErrorDescription()
        {
            switch (errorcode)
            {
                case 0:
                    message = "Cannot connect to server.\nContact Administrator!";
                    break;

                case 1045:
                    message = "Cannot connect to server.\nInvalid username or password.";
                    break;

                case 1042:
                    message = "Cannot connect to server.\nUnable to resolve DNS.\nCheck your internet connection and your DNS-settings,";
                    break;
                default:
                    message = "Cannot connect to server.\nContact Administrator!";
                    break;
            }
            return message;
        }

        private string getErrorHeader()
        {
            return "Connection failed!";
        }
    }
}
