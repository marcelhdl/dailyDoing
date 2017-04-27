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
        DBService db = new DBService("sae", "", "d7hevxduyf6mbuax.myfritz.net", "db_dailydoing", 8562); //3306
        //DBService db = new DBService("root", "", "localhost", "dailydoing", 3306);
        int userID;
        int contactID;
        MainWindow main;
        public UpdateContact(string firstname, string name, string email, int userID, int contactID, MainWindow main)
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
            this.main = main;
        }
        //Informationen aus Textboxen nehmen und Kontakt updaten
        private void btn_createUpdate_Click(object sender, RoutedEventArgs e)
        {
            db.updateContact(db.createconnectionstring(), contactID, userID, txt_Name.Text, txt_Firstname.Text, txt_email.Text);
            main.updateAllContactsBox();
            Close();
        }
    }
}
