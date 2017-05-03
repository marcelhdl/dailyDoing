using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDoing
{
    class LoginController
    {
        DBService db;
        protected string username;
        protected string pw;

        public LoginController(DBService db, string username, string pw)
        {
            this.db = db;
            this.username = username;
            this.pw = pw;
        }
        public bool isCorrectLogin() {
            if (db.getUserID(username) == -1) {
                return false;
            }
            return db.checkPassword(username, pw);
        }
    }
}
