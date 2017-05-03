﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDoing
{
    public class Contact
    {
        string name;
        string firstname;
        string email;
        string street;
        string houseNumber;
        string postCode;
        string city;
        string phoneNumber;
        string mobileNumber;
        int cid;
        int uid;

        #region getter/setter
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

        public string Street
        {
            get
            {
                return street;
            }

            set
            {
                street = value;
            }
        }

        public string HouseNumber
        {
            get
            {
                return houseNumber;
            }

            set
            {
                houseNumber = value;
            }
        }

        public string PostCode
        {
            get
            {
                return postCode;
            }

            set
            {
                postCode = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }

            set
            {
                phoneNumber = value;
            }
        }

        public string MobileNumber
        {
            get
            {
                return mobileNumber;
            }

            set
            {
                mobileNumber = value;
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

        public Contact(){}
#endregion
    }
}