using System;

namespace storygen
{
    class Origin
    {
        String OriginName;
        
        public Origin(String OriginName)
        {
            this.OriginName = OriginName;
        }

        public String getName() => OriginName;
    }
}
