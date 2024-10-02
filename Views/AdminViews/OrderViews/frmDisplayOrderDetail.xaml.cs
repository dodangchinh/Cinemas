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

namespace Chinh_C1_Cinemas
{
    /// <summary>
    /// Interaction logic for frmDisplayOrderDetail.xaml
    /// </summary>
    public partial class frmDisplayOrderDetail : Window
    {
        public frmDisplayOrderDetail(List<DetailOrder> lstDetailOrder)
        {
            InitializeComponent();

            dgOrderDetails.ItemsSource = lstDetailOrder;
        }
    }
}
