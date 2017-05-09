using DailyDoing.classes;
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
        int userID;
        Contact selectedContact;
        DBService db = new DBService();
        MainWindow main;
        ContactDAO contactService;
        public UpdateContact(MainWindow main, int userID)
        {
            InitializeComponent();
            this.userID = userID;
            txt_Name.Focus();
            this.main = main;
            contactService = new ContactDAO(userID, main);
            selectedContact = contactService.getSelectedContact();
            DetailView.DataContext = selectedContact;
        }
        //Informationen aus Textboxen nehmen und Kontakt updaten
        private void btn_createUpdate_Click(object sender, RoutedEventArgs e)
        {
            db.updateContact(selectedContact);
            contactService.fillContactsInListBox();
            Close();
        }
    }
}
