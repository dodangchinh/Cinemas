using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chinh_C1_Cinemas
{
    public class UnitOfWork
    {
        public static readonly object Instancelock = new object();
        public static UnitOfWork _Instance;

        public List<string> lstHeadlineDisplay = new List<string>();
        public List<string> lstNameMovie = new List<string>();


        public AccountRepository accountRepository { get; set; }
        public MovieRepository movieRepository { get; set; }
        public CinemaRepository cinemaRepository { get; set; }
        public ScheduleRepository scheduleRepository { get; set; }
        public ScheduleShowTimesRepository scheduleShowTimesRepository { get; set; }
        public OrdersRepository ordersRepository { get; set; }

        public static UnitOfWork Instance
        {
            get
            {
                lock (Instancelock)
                {
                    if (_Instance == null)
                        _Instance = new UnitOfWork();
                    return _Instance;
                }
            }
        }

        private UnitOfWork()
        {
            accountRepository = new AccountRepository();
            movieRepository = new MovieRepository();
            cinemaRepository = new CinemaRepository();
            scheduleRepository = new ScheduleRepository(movieRepository._lstMovie, cinemaRepository.lstCinema);
            scheduleShowTimesRepository = new ScheduleShowTimesRepository(scheduleRepository.lstSchedule);
            ordersRepository = new OrdersRepository(scheduleShowTimesRepository.listScheduleShowTimes);

            setHeadlineString();
            setNameMovie();
        }

        void setHeadlineString()
        {
            lstHeadlineDisplay.Add("Movies");
            lstHeadlineDisplay.Add("Cinemas");
            lstHeadlineDisplay.Add("Schedules");
            lstHeadlineDisplay.Add("ScheduleShowTimes");
            lstHeadlineDisplay.Add("Orders");
        }

        void setNameMovie()
        {
            foreach (var item in movieRepository.Gets())
                lstNameMovie.Add(item.Name);
        }

        
    }
}