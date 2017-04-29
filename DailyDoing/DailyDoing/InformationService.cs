using System;
using System.Collections.Generic;

namespace DailyDoing
{
    class InformationService
    {

        public InformationService(){}

        public List<Contact> getInfoForListBox(List<string[]> contactsPerUser)
        {
            List<Contact> contacts = new List<Contact>();
            foreach (string[] contactInfo in contactsPerUser)
            {
                contacts.Add(new Contact() { Name = contactInfo[2], Firstname = contactInfo[3], Email = contactInfo[4], Cid = Convert.ToInt32(contactInfo[0]), Uid = Convert.ToInt32(contactInfo[1]) });
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