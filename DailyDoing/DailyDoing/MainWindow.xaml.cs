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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace DailyDoing
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string username = String.Empty;
        string password = String.Empty;

        //DBService db = new DBService("sae", "sae123", "d7hevxduyf6mbuax.myfritz.net", "db_dailydoing", 3306); //8562
        DBService db = new DBService("root", "", "localhost", "dailydoing", 3306);

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            username = txt_username.Text;
            password = txt_password.Text;
            checkLogin();
            
        }
        private void checkLogin() {
            bool isCorrectLogin = db.checkLogin(db.createconnectionstring(),username,password); //Login prüfen
            if (isCorrectLogin) {
                MessageBox.Show("Successfully logged in!");
                getInfo(db);
            }
            else
            {
                MessageBox.Show(password + "Incorrect Login! Please try again!");
            }
        }
        private void getInfo(DBService db) {
            tab_contacts.IsSelected = true;
            InformationService infoService = new InformationService();
            List<string> allcontacts = infoService.getInfoForListBox(db.getContacts(db.createconnectionstring()));
            foreach (string contactName in allcontacts)
            {
                lBox_Kontakte.Items.Add(contactName);
            }
        }


        private void lBox_Kontakte_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lBox_Kontakte.SelectedItem != null)
            {
                List<string> allInfo =searchInfoForSelectedItem();
                
                txt_Name.Text = allInfo[2];
                txt_Firstname.Text = allInfo[3];
                txt_email.Text = allInfo[4];
            }
        }
        private List<string> searchInfoForSelectedItem() {
            int cid = Convert.ToInt32(lBox_Kontakte.SelectedItem.ToString().Substring(0,1));
            InformationService infoService = new InformationService();
            return infoService.getDetails(db.getContacts(db.createconnectionstring()),cid);
        }
    }
}
