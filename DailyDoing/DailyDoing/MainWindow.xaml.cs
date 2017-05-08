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
            btn_createContact.IsEnabled = false;
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
                fillContactsInListBox(db, db.getUserID(username));
                fillLendingsInListBox(db, db.getUserID(username));
            }
            else
            {
                MessageBox.Show("Incorrect Login! Please try again!");
            }

        }

        private void fillLendingsInListBox(DBService db, int userID)
        {
            List<Lending> lendings = new List<Lending>();
            foreach (string[] lendingInfo in db.getallLendings(userID))
            {
                lendings.Add(new Lending()
                {
                    Lid = Convert.ToInt32(lendingInfo[0]),
                    Uid = Convert.ToInt32(lendingInfo[1]),
                    Cid = Convert.ToInt32(lendingInfo[2]),
                    Title = lendingInfo[3],
                    Description = lendingInfo[4],
                    Category = lendingInfo[5],
                    Priority = lendingInfo[6],
                    Start = Convert.ToDateTime(lendingInfo[7]),
                    End = Convert.ToDateTime(lendingInfo[8]),
                    AllreadyBack = Convert.ToBoolean(lendingInfo[9])
                });
            }
            lBox_Lendings.ItemsSource = lendings;

        }

        //Logout and Reset all
        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            btn_login.Visibility = Visibility.Visible;
            btn_logout.Visibility = Visibility.Hidden;
            resetContactInfo();
            lBox_Kontakte.ItemsSource = null;
            lBox_Kontakte.Items.Clear();
            txt_password.Clear();
            txt_username.Clear();

        }
        private void mainwindow_KeyDown(object sender, KeyEventArgs e)
        {
            //Disslected Contact/Lending with ESC
            if (tab_contacts.IsSelected)
            {
                if (e.Key == Key.Escape)
                {
                    lBox_Kontakte.SelectedIndex = -1;
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

        private void resetContactInfo() //geht das auch über ItemSource = null ?
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
            List<Contact> allcontacts = infoService.contact_getInfoForListBox(db.getContacts(userID));
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
        //Details für Lending
        private void lBox_Lendings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lBox_Lendings.SelectedItem != null)
            { 
                
                //DetailViewLendings.DataContext = null;
                DetailViewLendings.DataContext = (Lending)lBox_Lendings.SelectedItem;
            }
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
            contact = (Contact)lBox_Kontakte.SelectedItem;
            db.deleteContact(contact.Cid);
            MessageBox.Show("Contact successfully deleted!");
            updateAllContactsBox();
        }
        //Einsteigspunkt für das Updaten eines vorhandenen Kontakts
        private void btn_updateContact_Click(object sender, RoutedEventArgs e)
        {
            int userID = db.getUserID(username);
            contact = (Contact)lBox_Kontakte.SelectedItem;
            UpdateContact update = new UpdateContact(contact, this);
            update.Show();
        }

        //Update nach SQL Verarbeitung

        public void updateAllContactsBox()
        {
            int userID = db.getUserID(username);
            fillContactsInListBox(db, userID);
        }

        
    }
}
