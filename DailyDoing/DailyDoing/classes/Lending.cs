using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyDoing.classes
{
    public class Lending
    {
        int lid;
        int uid;
        int cid;
        string title;
        string description;
        string category;
        string priority;
        DateTime start;
        DateTime end;
        bool allreadyBack;

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

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
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

        public DateTime Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }

        public DateTime End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
            }
        }

        public bool AllreadyBack
        {
            get
            {
                return allreadyBack;
            }

            set
            {
                allreadyBack = value;
            }
        }
    }
}
