using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using Chinh_C1_Cinemas;

namespace Chinh_C1_Cinemas
{
    /// <summary>
    /// Interaction logic for frmAdmin.xaml
    /// </summary>
    public partial class frmAdmin : Window
    {
        public MovieService movieService = new MovieService();

        ObservableCollection<string> lstHeadlineDisplay {  get; set; }
        ObservableCollection<string> lstNameMovie { get; set; }
        ObservableCollection<Movie> lstMovie {  get; set; }

        Button tempBtn = new Button();

        public frmAdmin(Account account)
        {
            InitializeComponent();
            txtNameAccount.Text += account.Name;

            lstHeadlineDisplay = new ObservableCollection<string>(UnitOfWork.Instance.lstHeadlineDisplay);
            lstNameMovie = new ObservableCollection<string>(UnitOfWork.Instance.lstNameMovie);
            lstNameMovie = new ObservableCollection<string>(UnitOfWork.Instance.lstNameMovie);

            Button button1 = this.FindName("btnCinemas") as Button;
            tempBtn.Background = button1.Background;
            tempBtn.Foreground = button1.Foreground;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMovies_Click(object sender, RoutedEventArgs e)
        {
            CheckClick(sender, e);
            spMain.Children.Clear();
            UcDisplayMovie ucDisplayMovie = new UcDisplayMovie(spMain);
            spMain.Children.Add(ucDisplayMovie);
        }

        private void btnCinemas_Click(object sender, RoutedEventArgs e)
        {
            CheckClick(sender, e);
            spMain.Children.Clear();
            UcDisplayCinema ucDisplayCinema = new UcDisplayCinema();
            spMain.Children.Add(ucDisplayCinema);
        }

        private void btnSchedules_Click(object sender, RoutedEventArgs e)
        {
            CheckClick(sender, e);
            spMain.Children.Clear();
            UcDisplaySchedule ucDisplaySchedule = new UcDisplaySchedule();
            spMain.Children.Add(ucDisplaySchedule);
        }

        private void btnScheduleShowTimes_Click(object sender, RoutedEventArgs e)
        {
            CheckClick(sender, e);
            spMain.Children.Clear();
            UcDisplayScheduleShowTime ucDisplayScheduleShowTime = new UcDisplayScheduleShowTime();
            spMain.Children.Add(ucDisplayScheduleShowTime);
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            CheckClick(sender, e);
            spMain.Children.Clear();
            UcDisplayOrders ucDisplayOrder = new UcDisplayOrders();
            spMain.Children.Add(ucDisplayOrder);
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void CheckClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;             

            button.Background = Brushes.White;
            button.Foreground = Brushes.OrangeRed;

            foreach (Button button1 in Helper.FindVisualChildren<Button>(spBtnMenu))
            {
                if (button1.Background == Brushes.White && button1 != button)
                {
                    button1.Background = tempBtn.Background;
                    button1.Foreground = tempBtn.Foreground;
                }

            }
        }
    }
}
