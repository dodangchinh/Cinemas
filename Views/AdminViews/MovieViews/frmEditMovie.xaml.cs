using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for frmEditMovie.xaml
    /// </summary>
    public partial class frmEditMovie : Window
    {
        Movie movie {  get; set; }
        MovieService movieService = new MovieService();
        ScheduleService scheduleService = new ScheduleService();
        ScheduleShowTimeService scheduleShowTimeService = new ScheduleShowTimeService();
        OrderService orderService = new OrderService(); 
        ObservableCollection<Movie> lstMovie { get; set; }
        public frmEditMovie(ObservableCollection<Movie> _lstMovie, Movie movie)
        {
            InitializeComponent();
            this.movie = movie;
            lstMovie = _lstMovie;
            txtId.Text = movie.Id;
            txtName.Text = movie.Name;
            txtDescription.Text = movie.Description;
            txtDuration.Text = movie.Duration.ToString();
            txtDuration.PreviewTextInput += TxtDuration_PreviewTextInput;
        }

        private void TxtDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            movie.Name = txtName.Text;
            movie.Description = txtDescription.Text;
            movie.Duration = int.Parse(txtDuration.Text);  
            movieService.Update(movie);
            scheduleService.UpdateMovie(movie);
            scheduleShowTimeService.UpdateMovie(movie);
            orderService.UpdateMovie(movie);
            MessageBox.Show($"This movie '{Name}' edit to the success");
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
