using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class ScheduleService
    {
        public bool Add(Schedule schedule)
        {
            if (!isExist(schedule.Movie.Id, schedule.Cinema.Id, schedule.AirDate))
            {
                UnitOfWork.Instance.scheduleRepository.Add(schedule);
                return true;
            }
            return false;
        }

        public void Update(Schedule schedule)
        {
            UnitOfWork.Instance.scheduleRepository.Update(schedule);
        }

        public Schedule getSchedule(string id)
        {
            return UnitOfWork.Instance.scheduleRepository.Get(id);
        }

        public void UpdateStatusByMovie(Movie movie)
        {
            foreach (var item in UnitOfWork.Instance.scheduleRepository.Gets())
                if (movie.Id.ToLower().CompareTo(item.Movie.Id.ToLower()) == 0
                    && item.AirDate > DateTime.Now)
                {
                    item.Status = movie.Status;
                    UnitOfWork.Instance.scheduleRepository.Update(item);
                }                               
        }

        public void UpdateMovie(Movie movie)
        {
            foreach (var item in UnitOfWork.Instance.scheduleRepository.Gets())
                if (movie.Id.ToLower().CompareTo(item.Movie.Id.ToLower()) == 0)
                {
                    item.Movie = movie;
                    UnitOfWork.Instance.scheduleRepository.Update(item);
                }
        }

        public void DeleteByMovie(Movie movie)
        {
            List<Schedule> lstSchedule = UnitOfWork.Instance.scheduleRepository.Gets();
            for(int i = 0; i < lstSchedule.Count; i++)
            {
                if (movie.Id.ToLower().CompareTo(lstSchedule[i].Movie.Id.ToLower()) == 0)
                {
                    UnitOfWork.Instance.scheduleRepository.Delete(lstSchedule[i]);

                    if (i >= lstSchedule.Count)
                        break;
                    i--;
                }                    
            }             
        }

        public Schedule SearchByMovie(string IdMovie, string IdCinema, DateTime date)
        {
            foreach (var item in UnitOfWork.Instance.scheduleRepository.Gets())
                if (item.Movie.Id.ToLower().CompareTo(IdMovie.ToLower()) == 0
                    && item.Cinema.Id.ToLower().CompareTo(IdCinema.ToLower()) == 0    
                    && item.AirDate.Date.CompareTo(date.Date) == 0)
                    return item;
            return null;
        }

        public bool isExist(string IdMovie, string IdCinema, DateTime date)
        {
            if (SearchByMovie(IdMovie, IdCinema, date) == null)
                return false;
            return true;
        }

        public List<Schedule> Gets()
        {
            return UnitOfWork.Instance.scheduleRepository.Gets();
        }

        public List<Schedule> GetByDate(DateTime date)
        {
            List<Schedule> lstSearch = new List<Schedule>();
            foreach (var item in UnitOfWork.Instance.scheduleRepository.Gets())
                if (item.AirDate.Date == date.Date)
                    lstSearch.Add(item);
            if (lstSearch.Count > 0)
                return lstSearch;
            return null;
        }

        public List<Schedule> GetByMovie(string name)
        {
            List<Schedule> lstSearch = new List<Schedule>();
            foreach (var item in UnitOfWork.Instance.scheduleRepository.Gets())
                if (item.Movie.Name.CompareTo(name) == 0)
                    lstSearch.Add(item);
            if (lstSearch.Count > 0)
                return lstSearch;
            return null;
        }

        public List<Schedule> GetByCinema(string name)
        {
            List<Schedule> lstSearch = new List<Schedule>();
            foreach (var item in UnitOfWork.Instance.scheduleRepository.Gets())
                if (item.Cinema.Name.CompareTo(name) == 0)
                    lstSearch.Add(item);
            if (lstSearch.Count > 0)
                return lstSearch;
            return null;
        }
    }
}