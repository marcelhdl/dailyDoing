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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            username = txt_username.Text;
            password = txt_passwd.Text;
            checkLogin();
        }
        private void checkLogin() {
            //DBService db = new DBService("sae", "sae123", "d7hevxduyf6mbuax.myfritz.net", "db_dailydoing", 3306); //8562
            DBService db = new DBService("root", "", "localhost", "dailydoing", 3306);
            bool isCorrectLogin = db.checkLogin(db.createconnectionstring(),username,password);
            if (isCorrectLogin) {
                MessageBox.Show("Successfully logged in!");
                getInfo(db);
            }
            else
            {
                MessageBox.Show("Incorrect Login! Please try again!");
            }
        }
        private void getInfo(DBService db) {
            tab_contacts.IsSelected = true;
            InformationService infoService = new InformationService();
            infoService.showInformation(db.getContacts(db.createconnectionstring()));
        }

    }
}
