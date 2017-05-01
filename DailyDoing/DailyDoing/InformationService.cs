using System;
using System.Collections.Generic;

namespace DailyDoing
{
    class InformationService
    {
        
        public InformationService(){}

        public List<Contact> contact_getInfoForListBox(List<string[]> contactsPerUser)
        {
            List<Contact> contacts = new List<Contact>();
            foreach (string[] contactInfo in contactsPerUser)
            {
                contacts.Add(new Contact() { Name = contactInfo[2], Firstname = contactInfo[3], Email = contactInfo[4], Cid = Convert.ToInt32(contactInfo[0]), Uid = Convert.ToInt32(contactInfo[1]) });
            }
            return contacts;
        }
        public List<string> contact_getDetails(string[] contactInfo)
        {
            List<string> contact_details = new List<string>();
            foreach (string info in contactInfo)
            {
                contact_details.Add(info);
            }
            return contact_details;
        }
        public List<Lending> lending_getInfoForListBox(List<string[]> lendingsPerUser)
        {
            List<Lending> lendings = new List<Lending>();
            foreach (string[] lendingInfo in lendingsPerUser)
            {
                lendings.Add(new Lending() { Title = lendingInfo[3], Timestamp_lend = lendingInfo[7], Lid = Convert.ToInt32(lendingInfo[0]), Cid = Convert.ToInt32(lendingInfo[2])});
            }
            return lendings;
        }
        public List<string> lending_getDetails(string[] lendingInfo)
        {
            List<string> lending_details = new List<string>();
            foreach (string info in lendingInfo)
            {
                lending_details.Add(info);
            }
            return lending_details;
        }


    }
}