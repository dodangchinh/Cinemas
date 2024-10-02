using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class Orders
    {
        public int Id { get; set; }
        public int IdScheduleShowTime { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string CinemaType { get; set; }
        public double Total { get; set; }

        public List<DetailOrder> lstDetailOrder { get; set; }
        public ScheduleShowTimes scheduleShowTimes { get; set; }
        public DateTime Date { get; set; }

        public Orders(int id, int idScheduleShowTime, string name, string phoneNumber, string cinemaType, double total, List<DetailOrder> lstDetailOrder, ScheduleShowTimes scheduleShowTimes, DateTime date)
        {
            Id = id;
            IdScheduleShowTime = idScheduleShowTime;
            Name = name;
            PhoneNumber = phoneNumber;
            CinemaType = cinemaType;
            Total = total;
            this.lstDetailOrder = Clone(lstDetailOrder);
            this.scheduleShowTimes = scheduleShowTimes.Clone();
            Date = date;
        }

        private List<DetailOrder> Clone(List<DetailOrder> lstDetailOrder)
        {
            List<DetailOrder> newList = new List<DetailOrder>();
            foreach (var item in lstDetailOrder)
                newList.Add(item.Clone());
            return newList;
        }

        public Orders(int Id, string Name, string Phone, ScheduleShowTimes scheduleShowTimes, DateTime date)
        {
            this.Id = Id;
            this.Name = Name;
            this.PhoneNumber = Phone;
            this.scheduleShowTimes = scheduleShowTimes.Clone();
            IdScheduleShowTime = scheduleShowTimes.Id;
            this.CinemaType = scheduleShowTimes.schedule.Cinema.Type;
            lstDetailOrder = new List<DetailOrder>();
            Date = date;
        }

        public Orders()
        {
            Date = DateTime.Now;
        }

        public void getTotal()
        {
            foreach (var item in lstDetailOrder)
                Total += item.Price - item.Discount;
        }

        public Orders Clone()
        {
            return new Orders(Id, IdScheduleShowTime, Name, PhoneNumber, CinemaType, Total, lstDetailOrder, scheduleShowTimes, Date);
        }
    }
}
