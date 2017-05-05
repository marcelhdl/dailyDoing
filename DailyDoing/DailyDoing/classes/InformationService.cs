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
                contacts.Add(new Contact() { Cid = Convert.ToInt32(contactInfo[0]), Uid = Convert.ToInt32(contactInfo[1]), Name = contactInfo[2],
                                             Firstname = contactInfo[3], Email = contactInfo[4], Street = contactInfo[5], HouseNumber = contactInfo[6],
                                             PostCode = contactInfo[7], City =  contactInfo[8], PhoneNumber = contactInfo[9], MobileNumber = contactInfo[10]});
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