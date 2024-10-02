using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class OrderService
    {
        public List<Orders> lstSearch {  get; set; }

        public OrderService() { }

        public void Add(Orders order)
        {
            UnitOfWork.Instance.ordersRepository.Add(order);
        }

        public Orders GetOrdersById(int Id)
        {
            foreach (var item in UnitOfWork.Instance.ordersRepository.Gets())
                if(item.Id == Id)
                    return item;
            return null;
        }

        public List<Orders> Gets()
        {
            return UnitOfWork.Instance.ordersRepository.Gets();
        }

        public List<Orders> GetOrdersByDate( DateTime date)
        {
            if (lstSearch == null)
                lstSearch = UnitOfWork.Instance.ordersRepository.Gets();
            List <Orders> lstSearchSecond = new List<Orders>();

            foreach (var item in lstSearch)
                if (item.Date.Date == date.Date)
                    lstSearchSecond.Add(item);

            if(lstSearchSecond.Count > 0)
                return lstSearch = lstSearchSecond;
            return lstSearch;
        }

        public List<Orders> GetOrdersByMovie(string name)
        {
            if (lstSearch == null)
                lstSearch = UnitOfWork.Instance.ordersRepository.Gets();
            List<Orders> lstSearchSecond = new List<Orders>();

            foreach (var item in lstSearch)
                if (item.scheduleShowTimes.schedule.Movie.Name.CompareTo(name) == 0)
                    lstSearchSecond.Add(item);

            if (lstSearchSecond.Count > 0)
                return lstSearch = lstSearchSecond;
            return lstSearch;
        }

        public void UpdateMovie(Movie movie)
        {
            foreach (var item in UnitOfWork.Instance.ordersRepository.Gets())
                if (movie.Id.ToLower().CompareTo(item.scheduleShowTimes.schedule.Movie.Id.ToLower()) == 0)
                {
                    item.scheduleShowTimes.schedule.Movie = movie;
                    UnitOfWork.Instance.ordersRepository.Update(item);
                }
        }

    }
}
