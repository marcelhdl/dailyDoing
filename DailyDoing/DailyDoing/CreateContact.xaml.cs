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
    /// Interaktionslogik für CreateContact.xaml
    /// </summary>
    public partial class CreateContact : Window
    {
        DBService db = new DBService();
        int userID;
        MainWindow main;
        Contact newContact = new Contact();

        public CreateContact(int userID, MainWindow main)
        {
            InitializeComponent();
            
            NewContactInfo.DataContext = newContact;
            this.userID = userID;
            this.main = main;
        }
        //Informationen aus Textboxen nehmen und daraus einen neuen Kontakt erzeugen
        private void btn_createContact_Click(object sender, RoutedEventArgs e)
        {
            newContact.Uid = userID;
            db.createContact(db.createconnectionstring(), newContact, userID);
            main.updateAllContactsBox();
            Close();
            
        }
    }
}
