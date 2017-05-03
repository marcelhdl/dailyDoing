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
        Contact contact;

        public MainWindow()
        {
            InitializeComponent();
            txt_username.Focus();
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
                btn_login.Visibility = Visibility.Hidden;
                btn_logout.Visibility = Visibility.Visible;
                btn_createContact.IsEnabled = true;
                fillContactsInListBox(db, db.getUserID(db.createconnectionstring(), username));
                fillLendingsInListBox(db, db.getUserID(db.createconnectionstring(), username));
            }
            else
            {
                MessageBox.Show("Incorrect Login! Please try again!");
            }

        }
        //Logout and Reset all
        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            btn_login.Visibility = Visibility.Visible;
            btn_logout.Visibility = Visibility.Hidden;
            btn_createContact.IsEnabled = false;
            resetContactInfo();
            resetLendingInfo();
            lb_lendings.ItemsSource = null;
            lBox_Kontakte.ItemsSource = null;
            lb_lendings.Items.Clear();
            lBox_Kontakte.Items.Clear();
            txt_password.Clear();
            txt_username.Clear();

        }
        private void mainwindow_KeyDown(object sender, KeyEventArgs e)
        {
            //Disslected Contact/Lending with ESC
            if (tab_contacts.IsSelected || tab_lendings.IsSelected)
            {
                if (e.Key == Key.Escape)
                {
                    lBox_Kontakte.SelectedIndex = -1;
                    lb_lendings.SelectedIndex = -1;
                }
            }
            //Login prüfen mit ENTER
            if (tab_login.IsSelected)
            {
                if (e.Key == Key.Enter)
                {
                    btn_login_Click(sender, e);
                }
            }
        }
        //Prüfen welcher Button aktiv sein soll und aktualisieren der Details bei Änderung der Auswahl
        private void lBox_Kontakte_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btn_deleteContact.IsEnabled = (lBox_Kontakte.SelectedItem != null);
            btn_updateContact.IsEnabled = (lBox_Kontakte.SelectedItem != null);
            if (lBox_Kontakte.SelectedItem == null)
            {
                resetContactInfo();
                return;
            }
            contact_getDetails((Contact)lBox_Kontakte.SelectedItem);

        }

        private void resetContactInfo()
        {
            txt_Name.Clear();
            txt_Firstname.Clear();
            txt_email.Clear();
            txt_city.Clear();
            txt_House_No.Clear();
            txt_mobilePhone.Clear();
            txt_phonenumber.Clear();
            txt_postcode.Clear();
            txt_street.Clear();
        }

        public void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void fillContactsInListBox(DBService db, int userID)
        {
            tab_contacts.IsSelected = true;
            InformationService infoService = new InformationService();
            List<Contact> allcontacts = infoService.contact_getInfoForListBox(db.getContacts(db.createconnectionstring(), userID));
            lBox_Kontakte.ItemsSource = allcontacts;

        }
        //Holen der Details eines Kontakts
        private void contact_getDetails(Contact contact)
        {
            if (lBox_Kontakte.SelectedItem != null)
            {
                DetailView.DataContext = contact;
            }
        }
        //List Box mit Übersicht der Kontakte befüllen
        private void fillLendingsInListBox(DBService db, int userID)
        {
            InformationService infoService = new InformationService();
            List<Lending> alllendings = infoService.lending_getInfoForListBox(db.getallLendings(db.createconnectionstring(), userID));
            lb_lendings.ItemsSource = alllendings;

        }
        
        private void lending_getDetails()
        {
            if (lb_lendings.SelectedItem != null)
            {
                List<string> allInfo = searchInfoForSelectedLending();

                InformationService infoService = new InformationService();
                List<string> contactinfo = infoService.contact_getDetails(db.getDetailsFromContacts(db.createconnectionstring(), Convert.ToInt32(allInfo[2])));
                txt_Desc_lending.Text = allInfo[4];
                txt_Firstname_Lending.Text = contactinfo[2];
                txt_Name_Lending.Text = contactinfo[3];
                txt_lendback_Lending.Text = allInfo[8];
                txt_lendtime_Lending.Text = allInfo[7];
                txt_getback_lending.Text = allInfo[9];
                txt_Category_lending.Text = allInfo[5];
                txt_priority_lending.Text = allInfo[6];
            }
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
            contact = (Contact)lBox_Kontakte.SelectedItem;
            db.deleteContact(db.createconnectionstring(), contact.Cid);
            MessageBox.Show("Contact successfully deleted!");
            updateAllContactsBox();
        }
        //Einsteigspunkt für das Updaten eines vorhandenen Kontakts
        private void btn_updateContact_Click(object sender, RoutedEventArgs e)
        {
            int userID = db.getUserID(db.createconnectionstring(), username);
            contact = (Contact)lBox_Kontakte.SelectedItem;
            UpdateContact update = new UpdateContact(contact, this);
            update.Show();
        }

        private void lb_lendings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_deleteLending.IsEnabled = (lb_lendings.SelectedItem != null);
            btn_updateLending.IsEnabled = (lb_lendings.SelectedItem != null);
            if (lb_lendings.SelectedItem == null)
            {
                resetLendingInfo();
                return;
            }
            lending_getDetails();
        }

        private void resetLendingInfo()
        {
            txt_Desc_lending.Clear();
            txt_Firstname_Lending.Clear();
            txt_Name_Lending.Clear();
            txt_lendback_Lending.Clear();
            txt_lendtime_Lending.Clear();
            txt_getback_lending.Clear();
            txt_Category_lending.Clear();
            txt_priority_lending.Clear();
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

        
    }
}
