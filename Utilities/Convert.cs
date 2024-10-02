using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public static class Convert
    {
        public static DataType ConvertTo(int type)
        {
            switch (type)
            {
                case 1:
                    return DataType.CinemaStandard;
                case 2:
                    return DataType.CinemaVip;
            }
            return DataType.Null;
        }
    }
}