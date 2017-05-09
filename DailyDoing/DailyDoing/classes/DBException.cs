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
                   MessageBox.Show("Cannot contact MySQL Server ");//MessageBox.Show("Cannot connect to server.  Contact administrator");
                   break;

                case 1045:
                    MessageBox.Show("Invalid username/password for DB-User, please try again");
                    break;

                case 1042:
                    caption = "Connection failed";
                    message = "Cannot connect to server.\nUnable to resolve DNS.\nCheck your internet connection and your DNS-settings\nReload?";
                    prepbox(caption, message);
                    break;
                default:
                    MessageBox.Show("Can't connect to MySQL-Server");
                    break;
            }

        }
        private void prepbox(string caption, string message)
        {

            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result;

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
