using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class ScheduleShowTimes
    {
        public int Id {  get; set; }    
        public Schedule schedule { get; set; }
        public DateTime AirDate { get; set; }
        public bool Status {  get; set; }

        public ScheduleShowTimes(int id, Schedule schedule, DateTime airDate, bool status)
        {
            Id = id;
            this.schedule = schedule.Clone();
            AirDate = airDate;
            Status = status;
        }

        public ScheduleShowTimes(int id, Schedule schedule, DateTime AirDate)
        {
            Id = id;
            this.schedule = schedule.Clone();
            this.AirDate = AirDate;
            this.Status = true;
        }

        public ScheduleShowTimes() { }

        public ScheduleShowTimes Clone()
        {
            return new ScheduleShowTimes(Id, schedule, AirDate, Status);
        }
    }
}
