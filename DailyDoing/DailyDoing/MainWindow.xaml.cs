using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
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
        DBService db = new DBService();
        LoginController loginController;

        public MainWindow()
        {
            InitializeComponent();
        }
        //Login prüfen mit Klick auf "Login"
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            username = txt_username.Text;
            password = txt_password.Password;
            loginController = new LoginController(db, username, password);
            if (loginController.isCorrectLogin())
            {
                MessageBox.Show("Successfully logged in!");
                fillContactsInListBox(db, db.getUserID(db.createconnectionstring(), username));
                fillLendingsInListBox(db, db.getUserID(db.createconnectionstring(), username));

            }
            else
            {
                MessageBox.Show("Incorrect Login! Please try again!");
            }

        }
        //Login prüfen mit EnterTaste
        private void txt_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btn_login_Click(sender, e);
            }
        }
        //List Box mit Übersicht der Kontakte befüllen
        private void fillContactsInListBox(DBService db, int userID)
        {
            tab_contacts.IsSelected = true;
            InformationService infoService = new InformationService();
            List<Contact> allcontacts = infoService.contact_getInfoForListBox(db.getContacts(db.createconnectionstring(), userID));
            lBox_Kontakte.ItemsSource = allcontacts;

        }
        private void fillLendingsInListBox(DBService db, int userID)
        {
            InformationService infoService = new InformationService();
            List<Lending> alllendings = infoService.lending_getInfoForListBox(db.getallLendings(db.createconnectionstring(), userID));
            lb_lendings.ItemsSource = alllendings;

        }
        //Holen der Details eines Kontakts
        private void contact_getDetails()
        {
            if (lBox_Kontakte.SelectedItem != null)
            {
                List<string> allInfo = searchInfoForSelectedContact();

                txt_Name.Text = allInfo[2];
                txt_Firstname.Text = allInfo[3];
                txt_email.Text = allInfo[4];
            }
        }
        private void lending_getDetails()
        {
            if (lb_lendings.SelectedItem != null)
            {
                List<string> allInfo = searchInfoForSelectedLending();

                txt_Desc_lending.Text = allInfo[4];
                txt_Firstname_Lending.Text = "needjoin";
                txt_Name_Lending.Text = "needjoin";
                txt_lendback_Lending.Text = allInfo[8];
                txt_lendtime_Lending.Text = allInfo[7];
                txt_getback_lending.Text = allInfo[9];
                txt_Category_lending.Text = allInfo[5];
                txt_priority_lending.Text = allInfo[6];
            }
        }

        //Suchen der Details eines Kontakts
        private List<string> searchInfoForSelectedContact()
        {
            Contact contact = (Contact)lBox_Kontakte.SelectedItem;
            int cid = contact.Cid;
            InformationService infoService = new InformationService();
            return infoService.contact_getDetails(db.getDetailsFromContacts(db.createconnectionstring(), cid));
        }
        private List<string> searchInfoForSelectedLending()
        {

            Lending lending = (Lending)lb_lendings.SelectedItem;
            int lid = lending.Lid;
            InformationService infoService = new InformationService();
            return infoService.lending_getDetails(db.getDetailsFromLending(db.createconnectionstring(), lid));
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
        //Prüfen welcher Button aktiv sein soll und aktualisieren der Details bei Änderung der Auswahl
        private void lBox_Kontakte_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btn_deleteContact.IsEnabled = (lBox_Kontakte.SelectedItem != null);
            btn_updateContact.IsEnabled = (lBox_Kontakte.SelectedItem != null);
            if (lBox_Kontakte.SelectedItem == null)
            {
                txt_Name.Clear();
                txt_Firstname.Clear();
                txt_email.Clear();
                return;
            }
            contact_getDetails();

        }
        private void lb_lendings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_deleteLending.IsEnabled = (lb_lendings.SelectedItem != null);
            btn_updateLending.IsEnabled = (lb_lendings.SelectedItem != null);
            if (lb_lendings.SelectedItem == null)
            {
                txt_Desc_lending.Clear();
                txt_Firstname_Lending.Clear();
                txt_Name_Lending.Clear();
                txt_lendback_Lending.Clear();
                txt_lendtime_Lending.Clear();
                txt_getback_lending.Clear();
                txt_Category_lending.Clear();
                txt_priority_lending.Clear();
                return;
            }
            lending_getDetails();
        }

        //Update nach SQL Verarbeitung

        public void updateAllContactsBox()
        {
            int userID = db.getUserID(db.createconnectionstring(), username);
            fillContactsInListBox(db, userID);
        }
        public void updateAllLendingsBox()
        {
            int userID = db.getUserID(db.createconnectionstring(), username);
            fillLendingsInListBox(db, userID);
        }
        public void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mainwindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                lBox_Kontakte.SelectedIndex = -1;
                lb_lendings.SelectedIndex = -1;
            }
        }


    }
}
