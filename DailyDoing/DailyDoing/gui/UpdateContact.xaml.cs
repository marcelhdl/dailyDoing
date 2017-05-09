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
            txt_Name.Focus();
            DetailView.DataContext = contact;
            this.main = main;
        }
        //Informationen aus Textboxen nehmen und Kontakt updaten
        private void btn_createUpdate_Click(object sender, RoutedEventArgs e)
        {
            db.updateContact(contact);
            main.updateAllContactsBox();
            Close();
        }
    }
}
