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
    /// MainController for DailyDoing
    /// </summary>
    public partial class MainWindow : Window
    {
        ContactDAO contactService;
        LendingDAO lendingService;
        GUIService guiService;
        User user;
        public MainWindow()
        {
            InitializeComponent();
            txt_username.Focus();
            guiService = new GUIService(this);
        }
        //Login prüfen mit Klick auf "Login"
        private void tryToLogin(object sender, RoutedEventArgs e)
        {
            user = new User(txt_username.Text);
            try
            {
                if (user.authenticate(txt_password.Password))
                {
                    user.IsLoggedIn = true;
                    guiService.switchBetweenLoggedInAndLoggedOut(user.IsLoggedIn);
                    contactService = new ContactDAO(this);
                    lendingService = new LendingDAO(this, contactService);
                    getInitialData();
                    setContext();
                }
            }
            catch (InvalidCredentialException)
            {
                txt_password.Clear();
                MessageBox.Show("Incorrect Login! Please try again!");
            }
        }

        private void setContext()
        {
            DetailView.DataContext = new Contact();
            DetailViewLendings.DataContext = new Lending();
            datePicker_end.SelectedDate = DateTime.Today;
            datePicker_start.SelectedDate = DateTime.Today;
            ContactInLending.DataContext = new Contact();
        }

        private void getInitialData()
        {
            contactService.fillContactsInListBox();
            lendingService.fillLendingsInListBox();
        }
        //Logout and Reset all
        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            user.IsLoggedIn = false;
            guiService.switchBetweenLoggedInAndLoggedOut(user.IsLoggedIn);
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
                    tryToLogin(sender, e);
                }
            }
        }
        //Close
        private void btn_exit_Click(object sender, RoutedEventArgs e)
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
            if (!contactService.createNewContactforUserInDB((Contact)DetailView.DataContext))
            {
                return;
            }
            contactService.resetContactInfo();
            contactService.fillContactsInListBox();
        }

        //Löschen eines vorhandenen Kontakts
        private void btn_deleteContact_Click(object sender, RoutedEventArgs e)
        {
            bool success = contactService.deleteContactFromDB();
            if (success) { 
            MessageBox.Show("Contact successfully deleted!");
            }
            contactService.fillContactsInListBox();
        }
        //Einstiegspunkt für das Updaten eines vorhandenen Kontakts
        private void btn_updateContact_Click(object sender, RoutedEventArgs e)
        {
            if (!contactService.updateSelectedContact((Contact)DetailView.DataContext))
            {
                return;
            }
            contactService.fillContactsInListBox();
        }
        //Doppel Klick auf Kontakt zum Erstellen eines Lendings für diesen
        private void lBox_Kontakte_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lBox_Contacts.SelectedItem != null)
            {
                bool itemSeleted = false;
                lendingService.resetLendingInfo();
                ContactInLending.DataContext = contactService.getSelectedContact();
                DetailViewLendings.DataContext = new Lending();
                datePicker_end.SelectedDate = DateTime.Today;
                datePicker_start.SelectedDate = DateTime.Today;
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
            if (!lendingService.createLending((Contact)ContactInLending.DataContext,(Lending)DetailViewLendings.DataContext)) {
                return;
            }
            lendingService.fillLendingsInListBox();
            lendingService.resetLendingInfo();
            return;

        }
        //Löscht vorhandenes Lending
        private void btn_deleteLending_Click(object sender, RoutedEventArgs e)
        {
            lendingService.deleteLending();
            lendingService.fillLendingsInListBox();
            lendingService.resetLendingInfo();
        }
        //Updated ein vorhandenes Lending
        private void btn_updateLending_Click(object sender, RoutedEventArgs e)
        {
            if (!lendingService.updateLending())
            {
                return;
            }
            lendingService.fillLendingsInListBox();
            lendingService.resetLendingInfo();
        }
        public int getCurrentUserID()
        {
            return user.UserID;
        }
    }
}
