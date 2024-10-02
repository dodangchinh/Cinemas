using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chinh_C1_Cinemas
{
    /// <summary>
    /// Interaction logic for UcDisplaySchedule.xaml
    /// </summary>
    public partial class UcDisplaySchedule : UserControl
    {
        CinemaService cinemaService = new CinemaService();
        MovieService movieService = new MovieService();
        ScheduleService scheduleService = new ScheduleService();

        ObservableCollection<Cinema> lstCinema { get; set; }
        ObservableCollection<Movie> lstMovie { get; set; }
        ObservableCollection<Schedule> lstSchedule {  get; set; }

        List<Schedule> lstSearch;
        public UcDisplaySchedule()
        {
            InitializeComponent();

            lstCinema = new ObservableCollection<Cinema>(UnitOfWork.Instance.cinemaRepository.Gets());
            cbFilterByCinema.ItemsSource = lstCinema;

            lstMovie = new ObservableCollection<Movie>(UnitOfWork.Instance.movieRepository.Gets());
            cbFilterByMovie.ItemsSource = lstMovie;

            lstSchedule = new ObservableCollection<Schedule>(scheduleService.Gets());
            dgSchedules.ItemsSource = lstSchedule;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddSchedule frmAddSchedule = new frmAddSchedule(lstSchedule);
            frmAddSchedule.ShowDialog();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            var itemM = cbFilterByMovie.SelectedItem as Movie;
            var itemC = cbFilterByCinema.SelectedItem as Cinema;

            if (datePicker.SelectedDate != null)
            {
                DateTime dateTime = datePicker.SelectedDate.Value;
                lstSearch = scheduleService.GetByDate(dateTime);
                if (lstSearch != null)
                    dgSchedules.ItemsSource = lstSearch;
            }

            if (itemM != null)
            {
                lstSearch = scheduleService.GetByMovie(itemM.Name);
                if (lstSearch != null)
                    dgSchedules.ItemsSource = lstSearch;
            }

            if (itemC != null)
            {
                lstSearch = scheduleService.GetByCinema(itemC.Name);
                if (lstSearch != null)
                    dgSchedules.ItemsSource = lstSearch;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            dgSchedules.ItemsSource = lstSchedule;
        }
    }
}
