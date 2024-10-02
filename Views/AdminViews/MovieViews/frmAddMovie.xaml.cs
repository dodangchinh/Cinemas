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
    /// Interaction logic for frmAddMovie.xaml
    /// </summary>
    public partial class frmAddMovie : Window
    {
        MovieService movieService = new MovieService();
        ObservableCollection<Movie> lstMovie {  get; set; } 
        public frmAddMovie(ObservableCollection<Movie> _lstMovie)
        {
            InitializeComponent();
            lstMovie = _lstMovie;
            txtId.Text = $"MV{Parameter.nMovie}";
            txtDuration.PreviewTextInput += TxtDuration_PreviewTextInput;
        }

        private void TxtDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string Id = txtId.Text;
            string Name = txtName.Text;
            string Description = txtDescription.Text;
            int Duration = int.Parse(txtDuration.Text);

            DateTime start = new DateTime();
            DateTime end = new DateTime();

            if(CheckValue(Name,ref start, ref end))
            {
                Movie movie = new Movie(Id, Name, Description, Duration, start, end);
                movieService.Add(movie);
                lstMovie.Add(movie);
                MessageBox.Show($"This movie '{Name}' added to the success");
                txtId.Text = $"MV{Parameter.nMovie}";
                Validator.CheckMovie();

            }           
        }

        bool CheckValue(string Name, ref DateTime start, ref DateTime end)
        {
            if (txtStartAirDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a Start date!");
                return false;
            }
            else if (txtEndAirDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a End date!");
                return false;
            }
            else
            {
                start = txtStartAirDate.SelectedDate.Value;
                end = txtEndAirDate.SelectedDate.Value;
                if (movieService.isExist(Name))
                {
                    MessageBox.Show($"The movie '{Name}' is Exist!");
                    return false;
                }
                else if (start.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Start Date must be greater than Now Date! Select again!");
                    return false;
                }
                else if (end.Date < start.Date)
                {
                    MessageBox.Show("End Date must be greater than Start Date! Select again!");
                    return false;
                }
            }
            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
