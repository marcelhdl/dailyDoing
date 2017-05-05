using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDoing.classes
{
    public partial class DBException:Exception
    {
        public DBException(Exception e)
        {
            e.GetBaseException()
        }
    }
}
