using Chinh_C1_Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class CinemaVip : Cinema
    {
        private double Surcharge = 40000;
        private double Fee = 100000;
        public DateTime date { get; set; }

        public CinemaVip(string Id, string Name, double Price, double Discount, string Type, bool Status, List<Seat> lstSeat, DateTime date)
        {
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
            this.Discount = Discount;
            this.Type = Type;
            this.Status = Status;
            this.date = date;
            this.lstSeat = CloneLstSeat(lstSeat);
        }

        public CinemaVip(string Id, string Name, double Price, double Discount, string Type, List<Seat> lstSeat) : base(Id, Name, Price, Discount, Type, lstSeat)
        {
            this.Price = Price;
            this.Discount = Discount;
            this.Type = Type = "Vip";
            lstSeat = new List<Seat>();
        }

        public CinemaVip()
        {
            Type = "Vip";
            lstSeat = new List<Seat>();
        }

        public override List<Seat> CloneLstSeat(List<Seat> lstSeat)
        {
            List<Seat> newLstSeat = new List<Seat>();
            foreach (var item in lstSeat)
                newLstSeat.Add(item.Clone());
            return newLstSeat;
        }

        public override void ChangeDiscount()
        {
            if (date.DayOfWeek.ToString().ToUpper().CompareTo("Thursday".ToUpper()) == 0)
                Discount = Fee * 0.1;
            else
                Discount = 0;
        }

        public override void ChangePrice()
        {
            Price = Fee + Surcharge;
        }

        public override Cinema Clone()
        {
            return new CinemaVip(this.Id, this.Name, this.Price, this.Discount, this.Type, this.Status, CloneLstSeat(lstSeat), date);
        }
    }
}
