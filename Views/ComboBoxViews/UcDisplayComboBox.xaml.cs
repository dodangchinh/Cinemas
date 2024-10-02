using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for UcDisplayComboBox4.xaml
    /// </summary>
    public partial class UcDisplayComboBox : UserControl
    {
        ObservableCollection<string> lstHeadlineDisplay;
        ObservableCollection<string> lstNameMovie;

        StackPanel spDisplayHead;
        StackPanel spDisplayBody;
        public UcDisplayComboBox(ObservableCollection<string> _lstHeadlineDisplay, ObservableCollection<string> _lstNameMovie, StackPanel _spDisplayHead, StackPanel _spDisplayBody)
        {
            InitializeComponent();
            lstHeadlineDisplay = _lstHeadlineDisplay;
            spDisplayHead = _spDisplayHead;
            spDisplayBody = _spDisplayBody;
            lstNameMovie = _lstNameMovie;
            cbDislpayLst.ItemsSource = lstHeadlineDisplay;
        }

        private void cbDislpayLst_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var item = cbDislpayLst.SelectedItem as string;

            switch (item)
            {
                case "Movies":
                    spDisplayBody.Children.Clear();
                    UcDisplayMovie ucDisplayMovie = new UcDisplayMovie(spDisplayHead, spDisplayBody);
                    spDisplayBody.Children.Add(ucDisplayMovie);
                    break;
                case "Cinemas":
                    spDisplayBody.Children.Clear();
                    UcDisplayCinema ucDisplayCinema = new UcDisplayCinema();
                    spDisplayBody.Children.Add(ucDisplayCinema);
                    break;
                case "Schedules":
                    spDisplayBody.Children.Clear();
                    UcDisplaySchedule ucDisplaySchedule = new UcDisplaySchedule();
                    spDisplayBody.Children.Add(ucDisplaySchedule);
                    break;
                case "ScheduleShowTimes":
                    spDisplayBody.Children.Clear();
                    UcDisplayScheduleShowTime ucDisplayScheduleShowTime = new UcDisplayScheduleShowTime();
                    spDisplayBody.Children.Add(ucDisplayScheduleShowTime);
                    break;
                case "Orders":
                    spDisplayBody.Children.Clear();
                    UcDisplayOrder ucDisplayOrder = new UcDisplayOrder(lstNameMovie);   
                    spDisplayBody.Children.Add(ucDisplayOrder);
                    break;
            }
        }


    }
}
