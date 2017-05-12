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
    /// Interaktionslogik für CreateContact.xaml
    /// </summary>
    public partial class CreateContact : Window
    {
        MainWindow main;
        ContactDAO contactService;
        Contact newContact = new Contact();

        public CreateContact(MainWindow main)
        {
            InitializeComponent();
            txt_Name.Focus();
            NewContactInfo.DataContext = newContact;
            this.main = main;
            contactService = new ContactDAO(main);
        }
        //Informationen aus Textboxen nehmen und daraus einen neuen Kontakt erzeugen
        private void btn_createContact_Click(object sender, RoutedEventArgs e)
        {
            newContact.Uid = main.getCurrentUserID();
            if (!contactService.createNewContactforUserInDB(newContact))
            {
                return;
            }
            contactService.fillContactsInListBox();
            Close();
            
        }
    }
}
