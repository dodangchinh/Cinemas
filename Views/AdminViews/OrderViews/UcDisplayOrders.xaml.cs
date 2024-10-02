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
    /// Interaction logic for UcDisplayOrder.xaml
    /// </summary>
    public partial class UcDisplayOrders : UserControl
    {
        OrderService orderService = new OrderService();

        ObservableCollection<Movie> lstMovie {  get; set; }
        ObservableCollection<Orders> lstOrder { get; set; }
        public UcDisplayOrders()
        {
            InitializeComponent();

            lstMovie = new ObservableCollection<Movie>(UnitOfWork.Instance.movieRepository.Gets());
            cbFilterByMovie.ItemsSource = lstMovie;

            lstOrder = new ObservableCollection<Orders>(orderService.Gets());
            dgOrders.ItemsSource = lstOrder;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            dgOrders.ItemsSource = lstOrder;

            datePicker.SelectedDate = null;
            cbFilterByMovie.SelectedItem = null;

            orderService.lstSearch = null;
        }
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            var item = cbFilterByMovie.SelectedItem as Movie;

            if (datePicker.SelectedDate != null)
            {
                DateTime dateTime = datePicker.SelectedDate.Value;
                List<Orders> lstSearch = orderService.GetOrdersByDate(dateTime);
                if (lstSearch != null)
                    dgOrders.ItemsSource = lstSearch;
            }

            if (item != null)
            {              
                List<Orders> lstSearch = orderService.GetOrdersByMovie(item.Name);
                if (lstSearch != null)
                    dgOrders.ItemsSource = lstSearch;
            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;
            Orders orders = item.DataContext as Orders;

            frmDisplayOrderDetail frmDisplayOrderDetail = new frmDisplayOrderDetail(orders.lstDetailOrder);
            frmDisplayOrderDetail.ShowDialog();
        }
    }
}
