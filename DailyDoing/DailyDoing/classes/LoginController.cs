using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace DailyDoing
{
    class LoginController
    {
        DBService db;
        protected string username;
        protected string pw;

        public LoginController(string username, string pw)
        {
            db = new DBService();
            this.username = username;
            this.pw = pw;
        }
        public bool authenticate() {
            if (db.getUserID(username) > 0 && db.checkPassword(username, pw) ){
                return true;
            }
            throw new InvalidCredentialException();
        }
    }
}
