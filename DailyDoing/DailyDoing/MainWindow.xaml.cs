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
        //Plausibilätscheck für Login
        private void checkLogin()
        {
            bool isCorrectLogin = db.checkLogin(db.createconnectionstring(), username, password);
            if (isCorrectLogin)
            {
                int userID = db.getUserID(db.createconnectionstring(), username);
                if (userID == -1)
                {
                    MessageBox.Show("DB Error, please contact Admin");
                }
                else
                {
                    MessageBox.Show("Successfully logged in!");
                    fillContactsToInListBox(db, userID);
                }
            }
            else
            {
                MessageBox.Show("Incorrect Login! Please try again!");
            }
        }
        //List Box mit Übersicht der Kontakte befüllen
        private void fillContactsToInListBox(DBService db, int userID)
        {
            lBox_Kontakte.Items.Clear();
            tab_contacts.IsSelected = true;
            InformationService infoService = new InformationService();
            List<string> allcontacts = infoService.getInfoForListBox(db.getContacts(db.createconnectionstring(), userID));
            foreach (string contactName in allcontacts)
            {
                lBox_Kontakte.Items.Add(contactName);
            }
        }
        //Zeigen der Details eines Kontakts nach Doppelklick auf selbigen
        private void lBox_Kontakte_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lBox_Kontakte.SelectedItem != null)
            {
                List<string> allInfo = searchInfoForSelectedContact();

                txt_Name.Text = allInfo[2];
                txt_Firstname.Text = allInfo[3];
                txt_email.Text = allInfo[4];
            }
        }
        //Suchen der Details eines Kontakts
        private List<string> searchInfoForSelectedContact()
        {
            int cid = Convert.ToInt32(lBox_Kontakte.SelectedItem.ToString().Substring(0, 1));
            InformationService infoService = new InformationService();
            return infoService.getDetails(db.getDetailsFromContacts(db.createconnectionstring(), cid));
        }
        //Einsteigspunkt für das Erstellen eines Neuen Kontakts
        private void btn_createContact_Click(object sender, RoutedEventArgs e)
        {
            int userID = db.getUserID(db.createconnectionstring(), username);
            CreateContact create = new CreateContact(userID, this);
            create.Show();
        }

        //Löschen eines vorhandenen Kontakts
        private void btn_deleteContact_Click(object sender, RoutedEventArgs e)
        {
            List<string> allInfo = searchInfoForSelectedContact();
            db.deleteContact(db.createconnectionstring(), Convert.ToInt32(allInfo[0]));
            MessageBox.Show("Contact successfully deleted!");
            updateAllContactsBox();
        }
        //Einsteigspunkt für das Updaten eines vorhandenen Kontakts
        private void btn_updateContact_Click(object sender, RoutedEventArgs e)
        {
            int userID = db.getUserID(db.createconnectionstring(), username);
            List<string> allInfo = searchInfoForSelectedContact();
            UpdateContact update = new UpdateContact(allInfo[3], allInfo[2], allInfo[4], userID, Convert.ToInt32(allInfo[0]), this);
            update.Show();
        }
        //Prüfen welcher Button aktiv sein soll
        private void lBox_Kontakte_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btn_deleteContact.IsEnabled = (lBox_Kontakte.SelectedItem != null);
            btn_updateContact.IsEnabled = (lBox_Kontakte.SelectedItem != null);
            if (lBox_Kontakte.SelectedItem == null)
            {
                txt_Name.Clear();
                txt_Firstname.Clear();
                txt_email.Clear();
            }
        }
        //Update nach SQL Verarbeitung
        public void updateAllContactsBox()
        {
            int userID = db.getUserID(db.createconnectionstring(), username);
            fillContactsToInListBox(db, userID);
        }
    }
}
