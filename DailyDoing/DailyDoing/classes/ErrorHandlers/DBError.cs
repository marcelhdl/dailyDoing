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
        public DBError(int errorcode)
        {
            this.errorHeader = getErrorHeader();
            this.errorcode = errorcode;
            this.errorDescription = getErrorDescription();
        }

        private string getErrorDescription()
        {
            switch (errorcode)
            {
                case 0:
                    errorDescription = "Cannot connect to server.\nContact Administrator!\nNumber zero";
                    break;

                case 1045:
                    errorDescription = "Cannot connect to server.\nInvalid username or password.";
                    break;

                case 1042:
                    errorDescription = "Cannot connect to server.\nUnable to resolve DNS.\nCheck your internet connection and your DNS-settings.";
                    break;

                case 1451:
                    errorDescription = "Constraint Fail\nPlease delete all contact specific lendings to delete the contact!";
                    break;

                default:
                    errorDescription = "Cannot connect to server.\nContact Administrator\nDefault!";
                    break;
            }
            return errorDescription;
        }

        private string getErrorHeader()
        {
            return "Invalid Action!";
        }
        /*public void setErrorCode(int errorCode) {
            this.errorcode = errorCode;
        }*/
    }
}
