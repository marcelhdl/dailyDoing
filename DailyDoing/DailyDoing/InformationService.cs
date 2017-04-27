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
        public List<string> getDetails(List<string[]> contactsPerUser, int cid)
        {
            List<string> details = new List<string>();
            foreach (string[] contactInfo in contactsPerUser)
            {
                if (cid == Convert.ToInt32(contactInfo[0]))
                {

                    foreach (string info in contactInfo)
                    {
                        details.Add(info);
                    }

                }

            }
            return details;

        }

    }
}
