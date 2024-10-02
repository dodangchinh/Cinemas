using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Interaction logic for UcLstMovie.xaml
    /// </summary>
    public partial class UcDisplayMovie : UserControl
    {
        MovieService movieService = new MovieService();
        ScheduleService scheduleService = new ScheduleService();
        ScheduleShowTimeService scheduleShowTimeService = new ScheduleShowTimeService();

        StackPanel spDisplayBody { get; set; }
        ObservableCollection<string> refreshList = new ObservableCollection<string>();
        ObservableCollection<Movie> lstMovie { get; set; }
        public UcDisplayMovie(StackPanel _spDisplayBody)
        {
            InitializeComponent();

            lstMovie = new ObservableCollection<Movie>(movieService.Gets());
            dgMovies.ItemsSource = lstMovie;

            spDisplayBody = _spDisplayBody;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddMovie frmAddMovie = new frmAddMovie(lstMovie);
            frmAddMovie.ShowDialog();
        }

        private void btnLock_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;
            Movie movie = item.DataContext as Movie;
            if (movie.Status)
                movie.Status = false;
            movieService.Update(movie);
            scheduleService.UpdateStatusByMovie(movie);
            scheduleShowTimeService.UpdateStatusByMovie(movie);

            dgMovies.ItemsSource = refreshList;
            dgMovies.ItemsSource = lstMovie;
        }

        private void btnActive_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;
            Movie movie = item.DataContext as Movie;
            if (!movie.Status)
                movie.Status = true;
            movieService.Update(movie);
            scheduleService.UpdateStatusByMovie(movie);
            scheduleShowTimeService.UpdateStatusByMovie(movie);

            dgMovies.ItemsSource = refreshList;
            dgMovies.ItemsSource = lstMovie;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;
            Movie movie = item.DataContext as Movie;

            frmEditMovie frmEditMovie = new frmEditMovie(lstMovie, movie);
            frmEditMovie.ShowDialog();

            dgMovies.ItemsSource = refreshList;
            dgMovies.ItemsSource = lstMovie;
        }
    }
}
