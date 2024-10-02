using Chinh_C1_Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class Schedule
    {
        public int Id { get; set; } 
        public Movie Movie { get; set; }
        public Cinema Cinema{ get; set; }
        public DateTime AirDate { get; set; }
        public bool Status { get; set; }

        public Schedule(int id, Movie movie, Cinema cinema, DateTime airDate, bool status)
        {
            Id = id;
            Movie = movie.Clone();
            Cinema = cinema.Clone();
            AirDate = airDate;
            Status = status;
        }

        public Schedule(int id, Movie movie, Cinema Cinema, DateTime AirDate) 
        {
            Parameter.nSchedule++;
            Id = id;
            Movie = movie.Clone();
            this.AirDate = AirDate;
            this.Cinema = Cinema.Clone();
            this.Status = true;
        }

        public Schedule(Cinema cinema)
        {
            Cinema newCinema = cinema.Clone();
            if(newCinema.Type.CompareTo("Standard") == 0)
                Cinema = new CinemaStandard(newCinema.Id, newCinema.Name, newCinema.Age, newCinema.Price, newCinema.Discount, newCinema.Type, newCinema.Status, newCinema.lstSeat);
            else
                Cinema = new CinemaVip(newCinema.Id, newCinema.Name, newCinema.Price, newCinema.Discount, newCinema.Type, newCinema.Status, newCinema.lstSeat, AirDate);   
        }

        public Schedule Clone()
        {
            return new Schedule(Id, Movie, Cinema, AirDate, Status);
        }
    }
}