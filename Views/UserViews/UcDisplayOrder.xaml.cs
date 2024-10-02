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
    /// Interaction logic for UcDisplayOrder.xaml
    /// </summary>
    public partial class UcDisplayOrder : UserControl
    {
        public event EventHandler Home;
        public UcDisplayOrder(Orders orders)
        {
            InitializeComponent();
            List<Orders> lstOrder = new List<Orders>();
            lstOrder.Add(orders);
            dgOrders.ItemsSource = lstOrder;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;
            Orders orders = item.DataContext as Orders;

            frmDisplayOrderDetail frmDisplayOrderDetail = new frmDisplayOrderDetail(orders.lstDetailOrder);
            frmDisplayOrderDetail.ShowDialog();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Home?.Invoke(this, EventArgs.Empty);
        }
    }
}
