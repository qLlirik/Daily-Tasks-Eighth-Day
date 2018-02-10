using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EighthDay.DB
{
    partial class Goods
    {
        public string YesString
        {
            get
            {
                return Yes ? "Есть" : "Нет";
            }
        }

        public string Info
        {
            get
            {
                return Name + '|' + Begin + '|' + End + '|' + Producer;
            }
        }
    }
}
