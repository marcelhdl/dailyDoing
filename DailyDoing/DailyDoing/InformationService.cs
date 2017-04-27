using System;
using System.Collections.Generic;

namespace DailyDoing
{
    class InformationService
    {

        public InformationService()
        {

        }
        public List<string> getInfoForListBox(List<string[]> contactsPerUser) {
            List<string> contacts = new List<string>();
            string result = String.Empty;
            foreach (string[] contactInfo in contactsPerUser)
            {
                result = contactInfo[0] + "\t" + contactInfo[2] + ", " + contactInfo[3];
                contacts.Add(result);
            }
            return contacts;

        }
        public List<string> getDetails(string[] contactInfo, int cid)
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