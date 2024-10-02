using Chinh_C1_Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class CinemaService
    {
        public List<Cinema> Gets()
        {
            return UnitOfWork.Instance.cinemaRepository.Gets();
        }

        public Cinema SearchById(string Id)
        {
            foreach (var item in UnitOfWork.Instance.cinemaRepository.lstCinema)
            {
                if (Id.ToLower().CompareTo(item.Id.ToLower()) == 0)
                    return item;
            }
            return null;
        }
    }
}
