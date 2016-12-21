using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Origin
    {
        String OriginName;
        
        public Origin(String OriginName)
        {
            this.OriginName = OriginName;
        }

        public String getName()
        {
            return OriginName;
        }
    }
}
