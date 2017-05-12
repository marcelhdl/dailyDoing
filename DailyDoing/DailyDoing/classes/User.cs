using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace DailyDoing.classes
{
    /// <summary>
    /// Object for Current User
    /// </summary>
    public class User
    {
        string username;
        int userID;
        bool isLoggedIn;
        DBService db;
        public User(string username)
        {
            db = new DBService();
            this.username = username;
            this.userID = db.getUserID(username);
        }
        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public int UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return isLoggedIn;
            }

            set
            {
                isLoggedIn = value;
            }
        }
        public bool authenticate(string pw)
        {
            if (UserID > 0 && db.checkPassword(Username, pw))
            {
                return true;
            }
            throw new InvalidCredentialException();
        }
    }
}
