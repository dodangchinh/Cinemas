using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chinh_C1_Cinemas
{
    public class Movie
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime StartAirDate { get; set; }
        public DateTime EndAirDate { get; set;}
        public bool Status { get; set; }

        public Movie(string Id, string name, string description, int duration, DateTime startAirDate, DateTime endAirDate, bool Status)
        {
            this.Id = Id;
            Name = name;
            Description = description;
            Duration = duration;
            StartAirDate = startAirDate;
            EndAirDate = endAirDate;
            this.Status = Status;
        }

        public Movie(string id, string name, string description, int duration, DateTime startAirDate, DateTime endAirDate)
        {
            Parameter.nMovie++;
            Id = id;
            Name = name;
            Description = description;
            Duration = duration;
            StartAirDate = startAirDate;
            EndAirDate = endAirDate;
        }

        public Movie() { }

        public Movie Clone()
        {
            return new Movie(this.Id, this.Name, this.Description, this.Duration, this.StartAirDate, this.EndAirDate, this.Status);
        }
    }
}
