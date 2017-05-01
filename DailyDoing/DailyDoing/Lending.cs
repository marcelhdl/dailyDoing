using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDoing
{
    class Lending
    {
        int lid;
        int cid;
        int uid;
        string title;
        string desc;
        string category;
        string priority;
        string timestamp_lend;
        string timestamp_lendback;
        bool getback;

        public int Lid
        {
            get
            {
                return lid;
            }

            set
            {
                lid = value;
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

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string Desc
        {
            get
            {
                return desc;
            }

            set
            {
                desc = value;
            }
        }

        public string Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
            }
        }

        public string Priority
        {
            get
            {
                return priority;
            }

            set
            {
                priority = value;
            }
        }

        public string Timestamp_lend
        {
            get
            {
                return timestamp_lend;
            }

            set
            {
                timestamp_lend = value;
            }
        }

        public string Timestamp_lendback
        {
            get
            {
                return timestamp_lendback;
            }

            set
            {
                timestamp_lendback = value;
            }
        }

        public bool Getback
        {
            get
            {
                return getback;
            }

            set
            {
                getback = value;
            }
        }

        public Lending(){}


    }
}
