using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            foreach ( string[] contactInfo in contactsPerUser)
            {
                result = contactInfo[2] + ", " + contactInfo[3];
                contacts.Add(result);
            }
            return contacts;
            
        }
    }
}
