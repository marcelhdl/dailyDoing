using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyDoing
{
    public partial class DBException:Exception
    {
        int errorcode;
        string caption, message;
        public DBException(int errorcode)
        {
            this.errorcode = errorcode;
            showerrmsg();
        }
        public void showerrmsg()
        {
            switch(errorcode)
            {
                case 0:
                    caption = "Connection failed";
                    message = "Cannot connect to server.\nContact Administrator!";
                    prepbox(0,caption, message);
                   break;

                case 1045:
                    caption = "Connection failed";
                    message = "Cannot connect to server.\nInvalid username or password.";
                    prepbox(0,caption, message);
                    break;

                case 1042:
                    caption = "Connection failed";
                    message = "Cannot connect to server.\nUnable to resolve DNS.\nCheck your internet connection and your DNS-settings\nReload?";
                    prepbox(1,caption, message);
                    break;
                default:
                    caption = "Connection failed";
                    message = "Cannot connect to server.\nContact Administrator!";
                    prepbox(0,caption, message);
                    break;
            }

        }
        private void prepbox(int mbb, string caption, string message)
        {
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxResult result;
            if (mbb == 1)
            {  
                buttons = MessageBoxButton.YesNo;
            }
            else if(mbb == 0)
            {
                 buttons = MessageBoxButton.OK;
            }

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == MessageBoxResult.Yes)
            {
                DBService db = new DBService();
                MainWindow main = new MainWindow();
                db.getUserID(main.txt_username.Text);
            }
        }
    }
}
