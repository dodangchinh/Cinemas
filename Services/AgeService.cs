using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class AgeService
    {
        public AgeService() { }

        public List<string> getListAge(int NumAdult, int NumChild)
        {
            List<string> lstAge = new List<string>();

            for (int i = 0; i < NumAdult; i++)
                lstAge.Add("Adult");

            for (int i = 0;i < NumChild; i++)
                lstAge.Add("Child");

            return lstAge;
        }
    }
}
