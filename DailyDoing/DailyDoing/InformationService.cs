using System;
using System.Collections.Generic;

namespace DailyDoing
{
    class InformationService
    {

        public InformationService()
        {

        }
        public List<Contact> getInfoForListBox(List<string[]> contactsPerUser) {
            List<Contact> contacts = new List<Contact>();
            foreach (string[] contactInfo in contactsPerUser)
            {
                Contact contact = new Contact(contactInfo[2], contactInfo[3], contactInfo[4], Convert.ToInt32(contactInfo[0]), Convert.ToInt32(contactInfo[1]));
                contacts.Add(contact);
            }
            return contacts;

        }
        public List<string> getDetails(string[] contactInfo)
        {
            List<string> details = new List<string>();

            foreach (string info in contactInfo)
            {
                details.Add(info);
            }
            return details;

        }

    }
}