using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chinh_C1_Cinemas.Models
{
    public class CinemaStandard : Cinema
    {
        private int Fee = 60000;

        public CinemaStandard(string Id, string Name, string Age, double Price, double Discount, string Type, bool Status, List<Seat> lstSeat)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
            this.Price = Price;
            this.Discount = Discount;
            this.Type = Type;
            this.Status = Status;
            this.lstSeat = CloneLstSeat(lstSeat);
        }

        public CinemaStandard(string Id, string Name, double Price, double Discount, string Type, List<Seat> lstSeat) : base(Id, Name, Price, Discount, Type, lstSeat)
        {
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
            this.Discount = Discount;
            this.Type = Type = "Standard";
            this.lstSeat = CloneLstSeat(lstSeat);
        }

        public CinemaStandard()
        {
            this.Type = "Standard";
            lstSeat = new List<Seat>();
        }

        public override Cinema Clone()
        {
            return new CinemaStandard(this.Id, this.Name, this.Age, this.Price, this.Discount, this.Type, this.Status, CloneLstSeat(lstSeat));
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
            if (Age.CompareTo("Child") == 0)
                Discount = Fee * 0.5;
            else
                Discount = 0;
        }

        public override void ChangePrice()
        {
            Price = Fee;
        }
    }
}
