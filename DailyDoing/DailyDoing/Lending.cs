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
        string desc;
        string category;
        string priority;
        string timestamp_lend;
        string timestamp_lendback;
        bool getback;

        public Lending(){}

        public int Lid { get => lid; set => lid = value; }
        public int Cid { get => cid; set => cid = value; }
        public int Uid { get => uid; set => uid = value; }
        public string Desc { get => desc; set => desc = value; }
        public string Category { get => category; set => category = value; }
        public string Priority { get => priority; set => priority = value; }
        public string Timestamp_lend { get => timestamp_lend; set => timestamp_lend = value; }
        public string Timestamp_lendback { get => timestamp_lendback; set => timestamp_lendback = value; }
        public bool Getback { get => getback; set => getback = value; }
    }
}
