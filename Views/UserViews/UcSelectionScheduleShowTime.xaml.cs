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
    /// Interaction logic for UcSelectionScheduleShowTime.xaml
    /// </summary>
    public partial class UcSelectionScheduleShowTime : UserControl
    {
        public event EventHandler SelectionChanged;
        public event EventHandler Back;
        public static ScheduleShowTimes scheduleShowTimes { get; set; }
        ScheduleShowTimeService scheduleShowTimeService = new ScheduleShowTimeService();
        List<ScheduleShowTimes> lstScheduleShowTimes { get; set; }
        StackPanel spDisplayBody {  get; set; }

        Button[] buttonDate;
        Button[] buttonTime;

        DateTime dateSelect;
        TimeSpan timeSelect;
        public UcSelectionScheduleShowTime(StackPanel spDisplayBody, Movie movie)
        {
            InitializeComponent();

            this.spDisplayBody = spDisplayBody;

            lstScheduleShowTimes = new List<ScheduleShowTimes>(scheduleShowTimeService.GetByMovie(movie.Name));
            InitListDate();
        }

        private void InitListDate()
        {
            int width = 80;
            int height = 30;
            int margin = 10;
            int padding = 5;

            buttonDate = new Button[Parameter.nDateSelect];
            DateTime dateTime = DateTime.Now;

            for (int i = 0; i < Parameter.nDateSelect; i++)
            {
                buttonDate[i] = new Button();

                buttonDate[i].Content = dateTime.Date.ToString("dd-MM-yyyy");
                buttonDate[i].Width = width;
                buttonDate[i].Height = height;
                buttonDate[i].Margin = new Thickness(margin);
                buttonDate[i].Padding = new Thickness(padding);
                buttonDate[i].BorderThickness = new Thickness(0);
                buttonDate[i].Background = Brushes.LightSkyBlue;

                buttonDate[i].Click += SelectedDate_Click;

                dateTime = dateTime.AddDays(1);
                tpSelecDate.Children.Add(buttonDate[i]);
            }


        }

        private void SelectedDate_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            item.Background = Brushes.DeepSkyBlue;

            ChangeBackgroundButton(item);
            AddTime(item);
             
            
        }

        private void ChangeBackgroundButton(Button item)
        {
            foreach (var item1 in buttonDate)
                if (item1.Background == Brushes.DeepSkyBlue && item1 != item)
                {
                    item1.Background = Brushes.LightSkyBlue;
                    break;
                }
        }

        private void AddTime(Button item)
        {
            int width = 50;
            int height = 30;
            int margin = 10;
            int padding = 5;

            dateSelect = DateTime.ParseExact(item.Content.ToString(), "dd-MM-yyyy", null);

            scheduleShowTimeService = new ScheduleShowTimeService();
            scheduleShowTimeService.lstSearch = lstScheduleShowTimes;
            List<ScheduleShowTimes> lstScheduleShowTimeForDate = scheduleShowTimeService.GetByDateSelect(dateSelect);

            tpSelecTime.Children.Clear();
            if (lstScheduleShowTimeForDate != null)
            {
                buttonTime = new Button[lstScheduleShowTimeForDate.Count];

                int i = 0;
                foreach (var item1 in lstScheduleShowTimeForDate)
                {
                    buttonTime[i] = new Button();

                    buttonTime[i].Content = item1.AirDate.ToString("HH:mm");
                    buttonTime[i].Width = width;
                    buttonTime[i].Height = height;
                    buttonTime[i].Margin = new Thickness(margin);
                    buttonTime[i].Padding = new Thickness(padding);
                    buttonTime[i].Background = Brushes.White;

                    buttonTime[i].Click += SelectedTime_Click; ;

                    tpSelecTime.Children.Add(buttonTime[i]);
                    i++;
                }
            }
            else
            {
                Label lb = new Label();
                lb.Content = "Oops, No showings today";
                tpSelecTime.Children.Add(lb);
            }
        }

        private void SelectedTime_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            timeSelect = TimeSpan.ParseExact(button.Content.ToString(), "hh\\:mm", null);
            dateSelect = dateSelect.Add(timeSelect);

            scheduleShowTimes = scheduleShowTimeService.GetByDateTimeSelect(dateSelect);

            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Back?.Invoke(this, EventArgs.Empty);
        }
    }
}
