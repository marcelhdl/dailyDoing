using DailyDoing.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MySql.Data;
using System.Linq;
using System.Security.Authentication;

namespace DailyDoing
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string username = String.Empty;
        int userID;
        bool isLoggedIn = false;
        DBService db = new DBService();
        LoginController loginController;
        Lending newLending;
        ContactDAO contactService;
        LendingDAO lendingService;
        GUIService guiService;
        public MainWindow()
        {
            InitializeComponent();
            txt_username.Focus();
            guiService = new GUIService(this);
        }
        //Login prüfen mit Klick auf "Login"
        private void tryToLogIn(object sender, RoutedEventArgs e)
        {
            username = txt_username.Text;
            loginController = new LoginController(username, txt_password.Password);
            try
            {
                isLoggedIn = loginController.authenticate();
                userID = db.getUserID(username);
                guiService.switchBetweenLoggedInAndLoggedOut(isLoggedIn);
                contactService = new ContactDAO(userID, this);
                lendingService = new LendingDAO(userID, this);
                getInitialData();
            }
            catch (InvalidCredentialException)
            {
                txt_password.Clear();
                MessageBox.Show("Incorrect Login! Please try again!");
            }
        }
        private void getInitialData()
        {
            contactService.fillContactsInListBox();
            lendingService.fillLendingsInListBox();
        }
        //Logout and Reset all
        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            isLoggedIn = false;
            guiService.switchBetweenLoggedInAndLoggedOut(isLoggedIn);
            contactService.resetContactInfo();
            lendingService.resetLendingInfo();
        }
        private void mainwindow_KeyDown(object sender, KeyEventArgs e)
        {
            //Disslected Contact/Lending with ESC
            if (tab_contacts.IsSelected)
            {
                if (e.Key == Key.Escape)
                {
                    lBox_Contacts.SelectedIndex = -1;
                }
            }
            if (tab_lendings.IsSelected)
            {
                if (e.Key == Key.Escape)
                {
                    lBox_Lendings.SelectedIndex = -1;
                    lendingService.resetLendingInfo();
                }
            }
            //Login prüfen mit ENTER
            if (tab_login.IsSelected)
            {
                if (e.Key == Key.Enter)
                {
                    tryToLogIn(sender, e);
                }
            }
        }
        //Close
        public void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //Prüfen welcher Button aktiv sein soll und aktualisieren der Details bei Änderung der Auswahl
        private void lBox_Kontakte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool itemSelected = (lBox_Contacts.SelectedItem != null);
            guiService.setButtonsForContact(itemSelected);
            if (!itemSelected)
            {
                contactService.resetContactInfo();
                return;
            }
            contactService.getDetailsForSelectedContact();
        }

        //Details für Lending
        private void lBox_Lendings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool itemSelected = (lBox_Lendings.SelectedItem != null);
            guiService.setButtonsForLending(itemSelected);
            if (!itemSelected)
            {
                lendingService.resetLendingInfo();
                return;
            }
            lendingService.setLendingInfo();
        }
        //Einsteigspunkt für das Erstellen eines Neuen Kontakts
        private void btn_createContact_Click(object sender, RoutedEventArgs e)
        {
            int userID = db.getUserID(username);
            CreateContact create = new CreateContact(userID, this);
            create.Show();
        }

        //Löschen eines vorhandenen Kontakts
        private void btn_deleteContact_Click(object sender, RoutedEventArgs e)
        {
            contactService.deleteContactFromDB();
            MessageBox.Show("Contact successfully deleted!");
            contactService.fillContactsInListBox();
        }
        //Einstiegspunkt für das Updaten eines vorhandenen Kontakts
        private void btn_updateContact_Click(object sender, RoutedEventArgs e)
        {
            int userID = db.getUserID(username);
            UpdateContact update = new UpdateContact(this, userID);
            update.Show();
        }
        //Doppel Klick auf Kontakt zum Erstellen eines Lendings für diesen
        private void lBox_Kontakte_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lBox_Contacts.SelectedItem != null)
            {
                bool itemSeleted = false;
                lendingService.resetLendingInfo();
                newLending = new Lending();
                ContactInLending.DataContext = contactService.getSelectedContact();
                newLending.Start = DateTime.Today;
                newLending.End = DateTime.Today;
                DetailViewLendings.DataContext = newLending;
                guiService.setButtonsForLending(itemSeleted);
                tab_lendings.IsSelected = true;
            }
        }
        //Erstellt neues Lending für Contact
        private void btn_createLending_Click(object sender, RoutedEventArgs e)
        {
            if (ContactInLending.DataContext == null)
            {
                MessageBox.Show("Please select the Target Contact with a Double Click on it");
                tab_contacts.IsSelected = true;
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are all Important fiels filled out?", "Check Fields", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                db.createLending(db.getUserID(username), (Contact)ContactInLending.DataContext, newLending);
                lendingService.fillLendingsInListBox();
                lendingService.resetLendingInfo();
                return;
            }
        }
        //Löscht vorhandenes Lending
        private void btn_deleteLending_Click(object sender, RoutedEventArgs e)
        {
            db.deleteLending(lendingService.getSelectedLending().Lid);
            lendingService.fillLendingsInListBox();
            lendingService.resetLendingInfo();
        }
        //Updated ein vorhandenes Lending
        private void btn_updateLending_Click(object sender, RoutedEventArgs e)
        {
            db.updateLending(lendingService.getSelectedLending());
            lendingService.fillLendingsInListBox();
            lendingService.resetLendingInfo();
        }
    }
}
