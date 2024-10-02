using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for frmUser.xaml
    /// </summary>
    public partial class frmUser : Window
    {
        OrderInformation orderInformation = new OrderInformation();
        Movie movie {  get; set; }  
        ScheduleShowTimes scheduleShowTimes {  get; set; }
        List<Seat> lstSeat { get; set; }
        List<string> lstAge { get; set; }

        ScheduleShowTimeService scheduleShowTimeService { get; set; }
        OrderService orderService { get; set; }

        public frmUser(Account account)
        {
            InitializeComponent();

            scheduleShowTimeService = new ScheduleShowTimeService();
            orderService = new OrderService();

            txtNameAccount.Text += account.Name;
            SelectionMovie(new object(), new EventArgs());
        }

        private void SelectionMovie(object sender, EventArgs e)
        {
            spMain.Children.Clear();
            UcSelectionMovie ucSelectionMovie = new UcSelectionMovie(spMain);
            ucSelectionMovie.SelectedMovie += SelectionScheduleShowTime;
            spMain.Children.Add(ucSelectionMovie);
            
        }

        private void SelectionScheduleShowTime(object sender, EventArgs e)
        {
            movie = UcSelectionMovie.movie;
            spMain.Children.Clear();
            UcSelectionScheduleShowTime ucSelectionScheduleShowTime = new UcSelectionScheduleShowTime(spMain, movie);
            ucSelectionScheduleShowTime.SelectionChanged += SelectionSeat;
            ucSelectionScheduleShowTime.Back += SelectionMovie;
            spMain.Children.Add(ucSelectionScheduleShowTime);
        }



        private void SelectionSeat(object sender, EventArgs e)
        {
            scheduleShowTimes = UcSelectionScheduleShowTime.scheduleShowTimes;        

            spMain.Children.Clear();
            UcSelectionSeat ucSelectionSeat = new UcSelectionSeat(scheduleShowTimes);
            ucSelectionSeat.SelectionChanged += SelectionAge;
            ucSelectionSeat.Back += SelectionScheduleShowTime;
            spMain.Children.Add(ucSelectionSeat);
        }

        private void SelectionAge(object sender, EventArgs e)
        {
            lstSeat = UcSelectionSeat.lstSeat;

            spMain.Children.Clear();
            UcSelectionAge ucSelectionAge = new UcSelectionAge(lstSeat);
            ucSelectionAge.Continue += Information;
            ucSelectionAge.Back += SelectionSeat;
            spMain.Children.Add(ucSelectionAge);
        }

        private void Information(object sender, EventArgs e)
        {
            lstAge = UcSelectionAge.lstAge;

            spMain.Children.Clear();
            UcInformation ucInformation = new UcInformation(spMain, orderInformation);
            ucInformation.InformationChanged += Pay;
            spMain.Children.Add(ucInformation);
        }

        private void Pay(object sender, EventArgs e)
        {
            Orders orders = new Orders(++Parameter.nOrders, orderInformation.Name, orderInformation.PhoneNumber, scheduleShowTimes, DateTime.Now);

            int n = 0;
            foreach (var item in lstSeat)
                orders.lstDetailOrder.Add(new DetailOrder(lstAge[n++], item.No, scheduleShowTimes.schedule));
            orders.getTotal();

            scheduleShowTimeService.Update(scheduleShowTimes);
            orderService.Add(orders);

            spMain.Children.Clear();
            UcDisplayOrder ucDisplayOrder = new UcDisplayOrder(orders);
            ucDisplayOrder.Home += SelectionMovie;
            spMain.Children.Add(ucDisplayOrder);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1280;
                    this.Height = 780;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximize = true;
                }
            }
        }
    }
}
