using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chinh_C1_Cinemas
{
    public static class Validator
    {
        static MovieService movieService = new MovieService(); 
        static ScheduleService scheduleService = new ScheduleService();
        static ScheduleShowTimeService scheduleShowTimeService = new ScheduleShowTimeService();

        public static void CheckMovie()
        {
            foreach (var item in UnitOfWork.Instance.movieRepository.Gets())
            {
                if (item.EndAirDate.Date < DateTime.Now)
                {
                    item.Status = false;
                    movieService.Update(item);
                    scheduleService.UpdateStatusByMovie(item);
                    scheduleShowTimeService.UpdateStatusByMovie(item);
                }
            }
        }

        public static void CheckSchedule()
        {
            foreach (var item in UnitOfWork.Instance.scheduleRepository.Gets())
                if(item.AirDate < DateTime.Now && item.Status)
                {
                    item.Status = false;
                    scheduleService.Update(item);
                }               
        }

        public static void CheckScheduleShowTime()
        {
            foreach (var item in UnitOfWork.Instance.scheduleShowTimesRepository.Gets())
                if (item.AirDate < DateTime.Now && item.Status)
                {
                    item.Status = false;
                    scheduleShowTimeService.Update(item);
                }
        }

        public static void CheckDate()
        {
            CheckMovie();
            CheckSchedule();
            CheckScheduleShowTime();
        }
    }
}
