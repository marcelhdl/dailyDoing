using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DailyDoing
{
    /// <summary>
    /// Interaktionslogik für CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        //DBService db = new DBService("sae", "sae123", "d7hevxduyf6mbuax.myfritz.net", "db_dailydoing", 3306); //8562
        DBService db = new DBService("root", "", "localhost", "dailydoing", 3306);
        int userID;
        public CreateUser(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void btn_createContact_Click(object sender, RoutedEventArgs e)
        {
            string firstname = txt_Firstname.Text;
            string name = txt_Name.Text;
            string email = txt_email.Text;
            db.createNewContact(db.createconnectionstring(),userID, name, firstname, email);
        }
    }
}
