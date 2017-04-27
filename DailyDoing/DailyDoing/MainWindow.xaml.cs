using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

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
        //Login prüfen mit Klick auf "Login"
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            username = txt_username.Text;
            password = txt_password.Password;
            checkLogin();
            
        }
        //Login prüfen mit EnterTaste
        private void txt_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                username = txt_username.Text;
                password = txt_password.Password;
                checkLogin();
            }
        }
        private void checkLogin() {
            bool isCorrectLogin = db.checkLogin(db.createconnectionstring(),username,password); //Login prüfen
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
                List<string> allInfo =searchInfoForSelectedContact();
                
                txt_Name.Text = allInfo[2];
                txt_Firstname.Text = allInfo[3];
                txt_email.Text = allInfo[4];
            }
        }
        private List<string> searchInfoForSelectedContact() {
            int cid = Convert.ToInt32(lBox_Kontakte.SelectedItem.ToString().Substring(0,1));
            InformationService infoService = new InformationService();
            return infoService.getDetails(db.getContacts(db.createconnectionstring()),cid);
        }
        
    }
}
