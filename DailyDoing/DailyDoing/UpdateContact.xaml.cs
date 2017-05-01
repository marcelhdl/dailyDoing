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
        Contact contact;
        DBService db = new DBService();
        MainWindow main;
        public UpdateContact(Contact contact, MainWindow main)
        {
            this.contact = contact;
            InitializeComponent();
            txt_Firstname.Text = contact.Firstname;
            txt_Name.Text = contact.Name;
            txt_email.Text = contact.Email;
            txt_street.Text = contact.Street;
            txt_House_No.Text = contact.HouseNumber.ToString();
            txt_postcode.Text = contact.PostCode.ToString();
            txt_city.Text = contact.City;
            txt_phonenumber.Text = contact.PhoneNumber;
            txt_mobilePhone.Text = contact.MobileNumber;
            this.main = main;
        }
        //Informationen aus Textboxen nehmen und Kontakt updaten
        private void btn_createUpdate_Click(object sender, RoutedEventArgs e)
        {
            db.updateContact(db.createconnectionstring(), contact.Cid, contact.Uid, txt_Name.Text, txt_Firstname.Text, txt_email.Text, txt_street.Text,Convert.ToInt32(txt_House_No.Text), Convert.ToInt32(txt_postcode.Text),txt_city.Text,txt_phonenumber.Text,txt_mobilePhone.Text);
            main.updateAllContactsBox();
            Close();
        }
    }
}
