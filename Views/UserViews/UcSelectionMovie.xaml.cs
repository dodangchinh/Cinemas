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
    /// Interaction logic for UcSelectionMovie.xaml
    /// </summary>
    public partial class UcSelectionMovie : UserControl
    {
        public event EventHandler SelectedMovie;

        public static Movie movie { get; set; }
        MovieService movieService = new MovieService();
        ObservableCollection<Movie> lstMovie {  get; set; }

        StackPanel spDisplayBody {  get; set; }
        public UcSelectionMovie(StackPanel spDisplayBody)
        {
            InitializeComponent();
            this.spDisplayBody = spDisplayBody;
            lstMovie = new ObservableCollection<Movie>(movieService.Gets());
            dgMovies.ItemsSource = lstMovie;
            dgMovies.SelectionChanged += MouseDouble_Click;
        }

        private void MouseDouble_Click(object sender, SelectionChangedEventArgs e)
        {
            if(sender is DataGrid dataGrid && dataGrid.SelectedItem != null)
            {
                movie = dataGrid.SelectedItem as Movie;
                SelectedMovie?.Invoke(this, EventArgs.Empty);
            }
                         
        }

    }
}
