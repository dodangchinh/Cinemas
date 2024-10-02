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
    /// Interaction logic for dataGrid.xaml
    /// </summary>
    public partial class dataGrid : UserControl
    {
        CinemaService cinemaService = new CinemaService();
        ObservableCollection<Cinema> lstCinema { get; set; }

        public dataGrid()
        {
            InitializeComponent();

            lstCinema = new ObservableCollection<Cinema>(cinemaService.Gets());
            membersDataGrid.ItemsSource = lstCinema;
        }
    }
}
