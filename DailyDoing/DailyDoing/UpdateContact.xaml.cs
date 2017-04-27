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
    /// Interaktionslogik für UpdateContact.xaml
    /// </summary>
    public partial class UpdateContact : Window
    {
        string firstname = String.Empty;
        string name = String.Empty;
        string email = String.Empty;
        //DBService db = new DBService("sae", "sae123", "d7hevxduyf6mbuax.myfritz.net", "db_dailydoing", 3306); //8562
        DBService db = new DBService("root", "", "localhost", "dailydoing", 3306);
        int userID;
        int contactID;
        public UpdateContact(string firstname, string name, string email, int userID, int contactID)
        {
            this.firstname = firstname;
            this.name = name;
            this.email = email;
            this.userID = userID;
            this.contactID = contactID;
            InitializeComponent();
            txt_Firstname.Text = firstname;
            txt_Name.Text = name;
            txt_email.Text = email;
        }

        private void btn_createUpdate_Click(object sender, RoutedEventArgs e)
        {
            db.updateContact(db.createconnectionstring(), contactID, userID, name, firstname, email);
            Close();
        }
    }
}
