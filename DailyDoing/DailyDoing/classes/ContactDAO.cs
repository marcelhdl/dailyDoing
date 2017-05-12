using DailyDoing.classes.ErrorHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyDoing.classes
{
    /// <summary>
    /// Gets, Manipulates and Sets Data from and to DataBase for Contacts
    /// </summary>
    class ContactDAO
    {
        public DBService db;
        MainWindow main;
        public ContactDAO(MainWindow main)
        {
            this.main = main;
            db = new DBService();
        }
        private List<Contact> getAllContactsForUser(List<string[]> contactsPerUser)
        {
            List<Contact> contacts = new List<Contact>();
            foreach (string[] contactInfo in contactsPerUser)
            {
                contacts.Add(getContact(Convert.ToInt32(contactInfo[0])));
            }
            return contacts;
        }
        public Contact getContact(int cid)
        {
            string[] contactInfo = db.getDetailsFromContacts(cid);
            Contact contact = new Contact()
            {
                Cid = Convert.ToInt32(contactInfo[0]),
                Uid = Convert.ToInt32(contactInfo[1]),
                Name = contactInfo[2],
                Firstname = contactInfo[3],
                Email = contactInfo[4],
                Street = contactInfo[5],
                HouseNumber = contactInfo[6],
                PostCode = contactInfo[7],
                City = contactInfo[8],
                PhoneNumber = contactInfo[9],
                MobileNumber = contactInfo[10]
            };
            return contact;
        }

        internal bool updateSelectedContact(Contact selectedContact)
        {
            if (hasMandatoryFieldsError(selectedContact))
            {
                return false;
            }
            db.updateContact(selectedContact);
            return true;        
        }

        internal bool createNewContactforUserInDB(Contact newContact)
        {
            if (hasMandatoryFieldsError(newContact)) {
                return false;
            }
            db.createContact(newContact);
            return true;
        }

        private bool hasMandatoryFieldsError(Contact newContact)
        {
            if (String.IsNullOrEmpty(newContact.Name) || String.IsNullOrEmpty(newContact.Firstname))
            {
                ContactError error = new ContactError();
                error.showErrorBox();
                return true;
            }
            return false;
        }

        public void fillContactsInListBox()
        {
            main.tab_contacts.IsSelected = true;
            List<Contact> allContacts = getAllContactsForUser(db.getContacts(main.getCurrentUserID())).OrderBy(contact => contact.Name).ToList();
            main.lBox_Contacts.ItemsSource = allContacts;

        }
        public void resetContactInfo()
        {
            main.DetailView.DataContext = null;
        }
        public void getDetailsForSelectedContact()
        {
            if (main.lBox_Contacts.SelectedItem != null)
            {
                main.DetailView.DataContext = getSelectedContact();
            }
        }
        public Contact getSelectedContact() {
            return (Contact)main.lBox_Contacts.SelectedItem;
        }
        public void deleteContactFromDB() {
            db.deleteContact(getSelectedContact().Cid);
        }
    }
}
