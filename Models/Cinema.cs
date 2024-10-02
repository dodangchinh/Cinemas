using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace Chinh_C1_Cinemas
{
    public abstract class Cinema
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; } = "";
        public double Price { get; set; }
        public double Discount {  get; set; }
        public string Type {  get; set; }
        public bool Status { get; set; }
        public List<Seat> lstSeat { get; set; }

        public Cinema(string Id, string Name, double Price, double Discount, string Type, List<Seat> lstSeat)
        {
            this.Id = Id;
            this.Name = Name;
            this.Discount = Discount;
            this.Price= Price;
            this.Type = Type;
            this.lstSeat = CloneLstSeat(lstSeat);
        }

        public Cinema()
        {
            lstSeat = new List<Seat>();
            Status = true;
        }

        public abstract List<Seat> CloneLstSeat(List<Seat> lstSeat);

        public abstract void ChangeDiscount();
        public abstract void ChangePrice();
        public abstract Cinema Clone();
    }
}
