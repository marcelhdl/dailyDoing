using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDoing
{
    class Contact
    {
        string name;
        string firstname;
        string email;
        int cid;
        int uid;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Firstname
        {
            get
            {
                return firstname;
            }

            set
            {
                firstname = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public int Cid
        {
            get
            {
                return cid;
            }

            set
            {
                cid = value;
            }
        }

        public int Uid
        {
            get
            {
                return uid;
            }

            set
            {
                uid = value;
            }
        }

        public Contact()//(string name, string firstname, string email, int cid, int uid)
        {
            //this.name = name;
            //this.firstname = firstname;
            //this.email = email;
            //this.cid = cid;
            //this.uid = uid;
        }
    }
}
