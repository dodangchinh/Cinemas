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
using System.Windows.Shapes;

namespace Chinh_C1_Cinemas
{
    /// <summary>
    /// Interaction logic for frmAddSchedule.xaml
    /// </summary>
    public partial class frmAddSchedule : Window
    {
        CinemaService cinemaService = new CinemaService();
        MovieService movieService = new MovieService();
        ScheduleService scheduleService = new ScheduleService();

        ObservableCollection<Cinema> lstCinema {  get; set; }
        ObservableCollection<Movie> lstMovie{ get; set; }
        ObservableCollection<Schedule> lstSchedule { get; set; }

        Cinema cinema { get; set; }
        Movie movie { get; set; }

        public frmAddSchedule(ObservableCollection<Schedule> lstSchedule)
        {
            InitializeComponent();
            ++Parameter.nSchedule;
            txtId.Text = Parameter.nSchedule.ToString();

            lstCinema = new ObservableCollection<Cinema>(cinemaService.Gets());
            lstMovie = new ObservableCollection<Movie>(movieService.Gets());
            this.lstSchedule = lstSchedule;

            cbListCinema.ItemsSource = lstCinema;
            cbListMovie.ItemsSource = lstMovie;
            cbListMovie.SelectionChanged += CbListMovie_SelectionChanged;
            
        }

        private void CbListMovie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item  = cbListMovie.SelectedItem as Movie;
            movie = movieService.SearchByName(item.Name);
            txtStartDate.Text = movie.StartAirDate.ToString("dd-MM-yyyy");
            txtEndDate.Text = movie.EndAirDate.ToString("dd-MM-yyyy");
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int Id = int.Parse(txtId.Text);

            var itemCinema = cbListCinema.SelectedItem as Cinema;
            cinema = cinemaService.SearchById(itemCinema.Id);

            DateTime date = dpDate.SelectedDate.Value;
            if (CheckDate(date))
            {
                Schedule schedule = new Schedule(Id, movie, cinema.Clone(), date);
                if(!movie.Status)
                    MessageBox.Show("This Movie to Lock!");
                else if (scheduleService.Add(schedule))
                {
                    MessageBox.Show("This Schedule added to the success!");
                    lstSchedule.Add(schedule);
                    Validator.CheckSchedule();
                    this.Close();
                }                    
                else
                {
                    MessageBox.Show("This Schedule is Exist!");
                    Parameter.nSchedule--;
                }           
            }
            else
            {
                MessageBox.Show("This Schedule date must be within the Date range of the Movie!");
                Parameter.nSchedule--;
            }         
        }

        bool CheckDate(DateTime date)
        {
            if(date.Date >= movie.StartAirDate.Date
              && date.Date <= movie.EndAirDate.Date)
                return true;
            return false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Parameter.nSchedule--;
            this.Close();
        }
    }
}
