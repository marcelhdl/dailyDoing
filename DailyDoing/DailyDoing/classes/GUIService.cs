using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyDoing.classes
{
    /// <summary>
    /// Controlls GUI Appearance
    /// </summary>
    class GUIService
    {
        MainWindow main;
        public GUIService(MainWindow main)
        {
            this.main = main;
        }
        public void switchBetweenLoggedInAndLoggedOut(bool isLoggedIn)
        {
            if (isLoggedIn)
            {
                setGuiToLoggedIn();
                return;
            }
            setGuiToLoggedOut();
        }

        private void setGuiToLoggedIn()
        {
            main.btn_login.Visibility = Visibility.Hidden;
            main.btn_logout.Visibility = Visibility.Visible;
            main.btn_createContact.IsEnabled = true;
        }

        private void setGuiToLoggedOut()
        {
            main.btn_login.Visibility = Visibility.Visible;
            main.btn_logout.Visibility = Visibility.Hidden;
            resetContactTab();
            resetLendingsTab();
            main.txt_password.Clear();
            main.txt_username.Clear();
        }

        private void resetContactTab()
        {
            deactivateAllContactButtons();
            resetContactsList();
        }

        
        private void resetLendingsTab()
        {
            deactivateAllLendingButtons();
            resetLendingsList();
        }
        private void resetContactsList()
        {
            main.lBox_Contacts.ItemsSource = null;
            main.lBox_Contacts.Items.Clear();
        }

        private void deactivateAllContactButtons()
        {
            main.btn_createContact.IsEnabled = false;
            main.btn_deleteContact.IsEnabled = false;
            main.btn_updateContact.IsEnabled = false;
        }
        private void resetLendingsList()
        {
            main.lBox_Lendings.ItemsSource = null;
            main.lBox_Lendings.Items.Clear();
        }

        private void deactivateAllLendingButtons()
        {
            main.btn_createLending.IsEnabled = false;
            main.btn_deleteLending.IsEnabled = false;
            main.btn_updateLending.IsEnabled = false;
        }

        internal void setButtonsForContact(bool itemSelected)
        {
            main.btn_createContact.IsEnabled = !itemSelected;
            main.btn_deleteContact.IsEnabled = itemSelected;
            main.btn_updateContact.IsEnabled = itemSelected;
        }

        internal void setButtonsForLending(bool itemSelected)
        {
            main.btn_deleteLending.IsEnabled = itemSelected;
            main.btn_updateLending.IsEnabled = itemSelected;
            main.btn_createLending.IsEnabled = !itemSelected;
        }
    }
}
