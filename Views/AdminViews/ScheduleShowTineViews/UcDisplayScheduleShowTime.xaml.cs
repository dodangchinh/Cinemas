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
    /// Interaction logic for UcDisplayScheduleShowTime.xaml
    /// </summary>
    public partial class UcDisplayScheduleShowTime : UserControl
    {
        CinemaService cinemaService = new CinemaService();
        MovieService movieService = new MovieService();
        ScheduleShowTimeService scheduleShowTimeService = new ScheduleShowTimeService();

        List<ScheduleShowTimes> lstSearch;

        ObservableCollection<Cinema> lstCinema { get; set; }
        ObservableCollection<Movie> lstMovie { get; set; }
        ObservableCollection<ScheduleShowTimes> lstScheduleShowTimes {  get; set; }
        public UcDisplayScheduleShowTime()
        {
            InitializeComponent();
            lstCinema = new ObservableCollection<Cinema>(UnitOfWork.Instance.cinemaRepository.Gets());
            cbFilterByCinema.ItemsSource = lstCinema;

            lstMovie = new ObservableCollection<Movie>(UnitOfWork.Instance.movieRepository.Gets());
            cbFilterByMovie.ItemsSource = lstMovie;


            lstScheduleShowTimes = new ObservableCollection<ScheduleShowTimes>(scheduleShowTimeService.Gets());
            dgScheduleShowTimes.ItemsSource = lstScheduleShowTimes;  
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddScheduleShowTime frmAddScheduleShowTime = new frmAddScheduleShowTime(lstScheduleShowTimes);
            frmAddScheduleShowTime.ShowDialog();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            var itemM = cbFilterByMovie.SelectedItem as Movie;
            var itemC = cbFilterByCinema.SelectedItem as Cinema;

            if (datePicker.SelectedDate != null)
            {
                DateTime dateTime = datePicker.SelectedDate.Value;
                lstSearch = scheduleShowTimeService.GetByDate(dateTime);
                if (lstSearch != null)
                    dgScheduleShowTimes.ItemsSource = lstSearch;
            }

            if (itemM != null)
            {
                lstSearch = scheduleShowTimeService.GetByMovie(itemM.Name);
                if (lstSearch != null)
                    dgScheduleShowTimes.ItemsSource = lstSearch;
            }

            if (itemC != null)
            {
                lstSearch = scheduleShowTimeService.GetByCinema(itemC.Name);
                if (lstSearch != null)
                    dgScheduleShowTimes.ItemsSource = lstSearch;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            dgScheduleShowTimes.ItemsSource = lstScheduleShowTimes;

            datePicker.SelectedDate = null;
            cbFilterByMovie.SelectedItem = null;
            cbFilterByCinema.SelectedItem = null;

            scheduleShowTimeService.lstSearch = null;
        }
    }
}
