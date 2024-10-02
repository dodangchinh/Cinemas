using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chinh_C1_Cinemas
{
    public class ScheduleShowTimeService
    {
        public List<ScheduleShowTimes> lstSearch {  get; set; } 
        public bool Add(List<ScheduleShowTimes> lstScheduleShowTimes, ScheduleShowTimes scheduleShowTime, Schedule schedule, DateTime tempDate1)
        {
            if (isExist(lstScheduleShowTimes, schedule, tempDate1))
            {
                UnitOfWork.Instance.scheduleShowTimesRepository.Add(scheduleShowTime);
                return true;
            }
            return false;
        }

        public void Update(ScheduleShowTimes scheduleShowTimes)
        {
            UnitOfWork.Instance.scheduleShowTimesRepository.Update(scheduleShowTimes);
        }

        public void UpdateStatusByMovie(Movie movie)
        { 
            foreach (var item in UnitOfWork.Instance.scheduleShowTimesRepository.Gets())
            {
                if (movie.Id.CompareTo(item.schedule.Movie.Id) == 0
                    && item.AirDate > DateTime.Now)
                {
                    item.Status = movie.Status;
                    UnitOfWork.Instance.scheduleShowTimesRepository.Update(item);
                }
            }                          
        }

        public void UpdateMovie(Movie movie)
        {
            foreach (var item in UnitOfWork.Instance.scheduleShowTimesRepository.Gets())
            {
                if (movie.Id.CompareTo(item.schedule.Movie.Id) == 0)
                {
                    item.schedule.Movie = movie;
                    UnitOfWork.Instance.scheduleShowTimesRepository.Update(item);
                }
            }
        }

        public void DeleteByMovie(Movie movie)
        {
            List<ScheduleShowTimes> lstScheduleShowTime = UnitOfWork.Instance.scheduleShowTimesRepository.Gets();
            for (int i = 0; i < lstScheduleShowTime.Count; i++)
            {
                if (movie.Name.CompareTo(lstScheduleShowTime[i].schedule.Movie.Name) == 0)
                {
                    UnitOfWork.Instance.scheduleShowTimesRepository.Delete(lstScheduleShowTime[i]);

                    if (i >= lstScheduleShowTime.Count)
                        break;
                    i--;
                }
            }
        }

        public ScheduleShowTimes SearchById(string Id)
        {
            return UnitOfWork.Instance .scheduleShowTimesRepository.Get(Id);
        }

        public Seat GetSeatByNo(ScheduleShowTimes scheduleShowTimes, int SeatNo)
        {
            foreach (var item in scheduleShowTimes.schedule.Cinema.lstSeat)
                if(item.No == SeatNo)
                    return item;
            return null;
        }

        public Seat GetSeatByName(ScheduleShowTimes scheduleShowTimes, string name)
        {
            foreach (var item in scheduleShowTimes.schedule.Cinema.lstSeat)
                if (item.Name.CompareTo(name) == 0)
                    return item;
            return null;
        }

        public bool isExist(List<ScheduleShowTimes> lstScheduleShowTimes, Schedule schedule, DateTime tempDate)
        {
            int n = 0;
            foreach (var item in lstScheduleShowTimes)
            {
                if (item.schedule.Cinema.Id.ToLower().CompareTo(schedule.Cinema.Id.ToLower()) == 0 && item.Status)
                {
                    if (tempDate.Date.CompareTo(item.AirDate.Date) == 0)
                    {
                        DateTime tempDate2 = new DateTime(item.AirDate.Year, item.AirDate.Month, item.AirDate.Day, item.AirDate.Hour, item.AirDate.Minute, 0);
                        DateTime tempDate1 = tempDate2;
                        tempDate2 = tempDate2.AddMinutes(schedule.Movie.Duration + 45);

                        if (tempDate < tempDate2 && tempDate > tempDate1)
                            n++;
                    }
                }
            }
            if (n == 0)
                return true;
            return false;
        }

        public List<ScheduleShowTimes> Gets()
        {
            return UnitOfWork.Instance.scheduleShowTimesRepository.Gets();
        }

        public List<ScheduleShowTimes> GetByDate(DateTime date)
        {
            if (lstSearch == null)
                lstSearch = UnitOfWork.Instance.scheduleShowTimesRepository.Gets();
            List<ScheduleShowTimes> lstSearchSecond = new List<ScheduleShowTimes>();

            foreach (var item in lstSearch)
                if (item.AirDate.Date == date.Date)
                    lstSearchSecond.Add(item);

            if (lstSearchSecond.Count > 0)
                return lstSearch = lstSearchSecond;

            return lstSearch;
        }

        public List<ScheduleShowTimes> GetByDateSelect(DateTime date)
        {
            if (lstSearch == null)
                lstSearch = UnitOfWork.Instance.scheduleShowTimesRepository.Gets();
            List<ScheduleShowTimes> lstSearchSecond = new List<ScheduleShowTimes>();

            foreach (var item in lstSearch)
                if (item.AirDate.Date == date.Date && item.Status)
                    lstSearchSecond.Add(item);

            if (lstSearchSecond.Count > 0)
                return lstSearch = lstSearchSecond;

            return null;
        }

        public ScheduleShowTimes GetByDateTimeSelect(DateTime date)
        {
            foreach (var item in lstSearch)
                if (item.AirDate == date)
                    return item;
            return null;
        }


        public List<ScheduleShowTimes> GetByMovie(string name)
        {
            if (lstSearch == null)
                lstSearch = UnitOfWork.Instance.scheduleShowTimesRepository.Gets();
            List<ScheduleShowTimes> lstSearchSecond = new List<ScheduleShowTimes>();

            foreach (var item in lstSearch)
                if (item.schedule.Movie.Name.CompareTo(name) == 0)
                    lstSearchSecond.Add(item);

            if (lstSearchSecond.Count > 0)
                return lstSearch = lstSearchSecond;
            return lstSearch;
        }

        public List<ScheduleShowTimes> GetByCinema(string name)
        {
            if (lstSearch == null)
                lstSearch = UnitOfWork.Instance.scheduleShowTimesRepository.Gets();
            List<ScheduleShowTimes> lstSearchSecond = new List<ScheduleShowTimes>();

            foreach (var item in lstSearch)
                if (item.schedule.Cinema.Name.CompareTo(name) == 0)
                    lstSearchSecond.Add(item);
            if (lstSearchSecond.Count > 0)
                return lstSearch = lstSearchSecond;
            return lstSearch;
        }
    }
}
