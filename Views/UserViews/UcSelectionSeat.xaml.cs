using Chinh_C1_Cinemas.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chinh_C1_Cinemas
{
    /// <summary>
    /// Interaction logic for UcSelectionSeat.xaml
    /// </summary>
    public partial class UcSelectionSeat : UserControl
    {
        int rows = 4;
        int columns = 4;

        int width = 50;
        int height = 30;
        int margin = 10;
        int padding = 5;
        Button[][] buttons;

        public event EventHandler SelectionChanged;
        public event EventHandler Back;

        ScheduleShowTimes scheduleShowTimes;
        public static List<Seat> lstSeat {  get; set; }
        ScheduleShowTimeService scheduleShowTimeService;

        public UcSelectionSeat(ScheduleShowTimes scheduleShowTimes)
        {
            InitializeComponent();
            this.scheduleShowTimes = scheduleShowTimes;
            scheduleShowTimeService = new ScheduleShowTimeService();
            lstSeat = new List<Seat>();
            InitSeat();
        }

        private void InitSeat()
        {
            List<Seat> lstSeat = scheduleShowTimes.schedule.Cinema.lstSeat;
            buttons = new Button[rows][];
            int n = 0;
            for (int i = 0; i < rows; i++)
            {
                buttons[i] = new Button[columns];
                StackPanel stackPanel = new StackPanel();
                for (int j = 0; j < columns; j++)
                {
                    buttons[i][j] = new Button();
                    buttons[i][j].Content = lstSeat[n].Name;
                    buttons[i][j].Width = width;
                    buttons[i][j].Height = height;
                    buttons[i][j].Margin = new Thickness(margin);
                    buttons[i][j].Padding = new Thickness(padding);
                    buttons[i][j].BorderThickness = new Thickness(0);

                    if (lstSeat[n].Status)
                    {
                        if (scheduleShowTimes.schedule.Cinema is CinemaStandard)
                            buttons[i][j].Background = Brushes.Gray;
                        else
                            buttons[i][j].Background = Brushes.DarkRed;
                        buttons[i][j].Click += SelectionSeat_Click;
                    }                      
                    else
                        buttons[i][j].Background = Brushes.White;
                    
                    stackPanel.Children.Add(buttons[i][j]);
                    n++;
                }
                wpSeat.Children.Add(stackPanel);
            }
        }

        private void SelectionSeat_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Seat seat = scheduleShowTimeService.GetSeatByName(scheduleShowTimes, button.Content.ToString());

            if (button.Background == Brushes.Red)
            {
                if (scheduleShowTimes.schedule.Cinema is CinemaStandard)
                    button.Background = Brushes.Gray;
                else
                    button.Background = Brushes.DarkRed;

                seat.Status = true;
                lstSeat.Remove(seat);
            }       
            else
            {
                button.Background = Brushes.Red;
                seat.Status = false;
                lstSeat.Add(seat);
            }       
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Back?.Invoke(this, EventArgs.Empty);
        }
    }
}
