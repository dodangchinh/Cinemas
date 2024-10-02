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
    /// Interaction logic for UcInformation.xaml
    /// </summary>
    public partial class UcInformation : UserControl
    {
        public event EventHandler InformationChanged;
        OrderInformation orderInformation {  get; set; }
        StackPanel spDisplayBody {  get; set; }
        public UcInformation(StackPanel spDisplayBody, OrderInformation orderInformation)
        {
            InitializeComponent();
            this.spDisplayBody = spDisplayBody;
            this.orderInformation = orderInformation;
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            orderInformation.Name = txtName.Text;
            orderInformation.PhoneNumber = txtPhone.Text;
            InformationChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
