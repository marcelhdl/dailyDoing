using System;
using System.Text.RegularExpressions;

namespace DailyDoing.classes
{
    /// <summary>
    /// Validator for Email Addresses
    /// </summary>
    class EmailValidator
    {
        public static bool isValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch(FormatException)
            {

                return false;
            }
                
        }
    }
}