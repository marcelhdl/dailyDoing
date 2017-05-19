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
        //Holen der Kontakte aus der DB
        public void fillContactsInListBox()
        {
            main.tab_contacts.IsSelected = true;
            List<Contact> allContacts = getAllContactsForUser(db.getContacts(main.getCurrentUserID()));
            main.lBox_Contacts.ItemsSource = allContacts;

        }
        //Initialisieren der Kontaktliste und die einzelnen Informationen 
        //an die Felder des Kontakts hängen und sortierte Liste zurück geben
        private List<Contact> getAllContactsForUser(List<string[]> contactsPerUser)
        {
            List<Contact> contacts = new List<Contact>();
            foreach (string[] contactInfo in contactsPerUser)
            {
                contacts.Add(getContact(Convert.ToInt32(contactInfo[0])));
            }
            return contacts.OrderBy(contact => contact.Name).ToList();
        }
        //Felder des Kontakts füllen
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

        //Setzen des DataContext auf den gewählten Kontakt in der ListBox
        public void getDetailsForSelectedContact()
        {
            if (main.lBox_Contacts.SelectedItem != null)
            {
                main.DetailView.DataContext = getSelectedContact();
            }
        }

        //Bei Update prüfen auf Pflichtfelder und Datenbankzugriff für gewählten Kontakt
        internal bool updateSelectedContact(Contact selectedContact)
        {
            if (hasMandatoryFieldsError(selectedContact))
            {
                return false;
            }
            db.updateContact(selectedContact);
            return true;
        }
        //Neuen User anlegen wenn keine Pflichtfelder verletzt sind und Datenbankzugriff
        internal bool createNewContactforUserInDB(Contact newContact)
        {
            if (hasMandatoryFieldsError(newContact))
            {
                return false;
            }
            newContact.Uid = main.getCurrentUserID();
            db.createContact(newContact);
            return true;
        }
        //Löschen des gewählten Kontakts
        public bool deleteContactFromDB()
        {
           return db.deleteContact(getSelectedContact().Cid);
        }

        public void resetContactInfo()
        {
            main.DetailView.DataContext = new Contact();
        }
        //Über Binding Kontakt aus Liste zurückbekommen
        public Contact getSelectedContact()
        {
            return (Contact)main.lBox_Contacts.SelectedItem;
        }
        //Pflichtfelder prüfen für Kontakt
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
    }
}
