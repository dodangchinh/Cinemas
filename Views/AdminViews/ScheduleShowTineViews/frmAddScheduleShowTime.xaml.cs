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
using System.Windows.Shapes;

namespace Chinh_C1_Cinemas
{
    /// <summary>
    /// Interaction logic for frmAddScheduleShowTime.xaml
    /// </summary>
    public partial class frmAddScheduleShowTime : Window
    {
        ScheduleService scheduleService = new ScheduleService();
        ScheduleShowTimeService scheduleShowTimeService = new ScheduleShowTimeService();

        ObservableCollection<Schedule> lstSchedule {  get; set; }
        ObservableCollection<ScheduleShowTimes> lstScheduleShowTime {  get; set; }

        Schedule schedule {  get; set; }
        public frmAddScheduleShowTime(ObservableCollection<ScheduleShowTimes> lstScheduleShowTime)
        {
            InitializeComponent();
            InitializeTimeSelectors();

            ++Parameter.nSchedule;
            txtId.Text = Parameter.nSchedule.ToString();

            lstSchedule = new ObservableCollection<Schedule>(scheduleService.Gets());   
            this.lstScheduleShowTime = lstScheduleShowTime;

            dgSchedules.ItemsSource = lstSchedule;
            dgSchedules.SelectionChanged += dgSchedulesShowTime_SelectionChanged;
        }

        private void InitializeTimeSelectors()
        {
            for (int i = 0; i < 24; i++)
                cbHuor.Items.Add(i.ToString());
            for (int i = 0; i < 60; i++)
                cbMinute.Items.Add(i.ToString());
        }

        private void dgSchedulesShowTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            schedule = dgSchedules.SelectedItem as Schedule;
            if(!schedule.Status)
                MessageBox.Show("This Schedule to Lock!");
            else
                txtIdScheduleSelected.Text = schedule.Id.ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DateTime tempDate1 = new DateTime(schedule.AirDate.Year, schedule.AirDate.Month, schedule.AirDate.Day);

            TimeSpan time = new TimeSpan(int.Parse(cbHuor.Text), int.Parse(cbMinute.Text), 0);
            tempDate1 = tempDate1.Add(time);

            ScheduleShowTimes scheduleShowTimes = new ScheduleShowTimes(++Parameter.nScheduleShowTimes, schedule, tempDate1);
  
            if (scheduleShowTimeService.Add(scheduleShowTimeService.Gets(), scheduleShowTimes, schedule, tempDate1))
            {
                MessageBox.Show("This Schedule added to the success!");
                lstScheduleShowTime.Add(scheduleShowTimes);
                Validator.CheckScheduleShowTime();
                this.Close();
            }
            else
            {
                MessageBox.Show("This Schedule is Exist!");
                Parameter.nSchedule--;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Parameter.nSchedule--;
            this.Close();
        }
    }
}
