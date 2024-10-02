using Chinh_C1_Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class DetailOrder
    {
        public string Age { get; set; }
        public int SeatNo { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public Cinema cinema { get; set; }

        public DetailOrder(string Age, int SeatNo, double Price, double Discount, Cinema cinema)
        {
            this.Age = Age;
            this.SeatNo = SeatNo;
            this.Price = Price;
            this.Discount = Discount;
            this.cinema = cinema.Clone();
        }

        public DetailOrder(string Age, int SeatNo, Schedule schedule)
        {
            this.Age = Age;
            this.SeatNo = SeatNo;

            Cinema newCinema = schedule.Cinema.Clone();
            if (newCinema.Type.CompareTo("Standard") == 0)
                cinema = new CinemaStandard(newCinema.Id, newCinema.Name, newCinema.Age, newCinema.Price, newCinema.Discount, newCinema.Type, newCinema.Status, newCinema.lstSeat);
            else
                cinema = new CinemaVip(newCinema.Id, newCinema.Name, newCinema.Price, newCinema.Discount, newCinema.Type, newCinema.Status, newCinema.lstSeat, schedule.AirDate);

            this.cinema.Age = Age;
            schedule.Cinema.Age = Age;
            this.cinema.ChangePrice();
            Price = this.cinema.Price;
            this.cinema.ChangeDiscount();
            Discount = this.cinema.Discount;
        }

        public DetailOrder Clone()
        {
            return new DetailOrder(this.Age, this.SeatNo, this.Price, this.Discount, this.cinema);
        }
    }
}
