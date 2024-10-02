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
    /// Interaction logic for UcSelectionAge.xaml
    /// </summary>
    public partial class UcSelectionAge : UserControl
    {
        public event EventHandler Continue;
        public event EventHandler Back;
        List<Seat> lstSeat;
        public static List<string> lstAge {  get; set; }
        AgeService ageService = new AgeService();
        public static int NumAdult { get; set; }
        public static int NumChild {  get; set; }
        public UcSelectionAge(List<Seat> lstSeat)
        {
            InitializeComponent();
            NumChild = 0;
            NumAdult = 0;
            this.lstSeat = lstSeat;
            lbTotalSeat.Content += lstSeat.Count.ToString();

        }

        private void btnAdditionAdult_Click(object sender, RoutedEventArgs e)
        {
            if(NumAdult + NumChild < lstSeat.Count)
                txtNumAdult.Text = (++NumAdult).ToString();
            CheckBtnContinue();
        }

        private void btnSubtractionAdult_Click(object sender, RoutedEventArgs e)
        {
            if (NumAdult > 0)
                txtNumAdult.Text = (--NumAdult).ToString();
            CheckBtnContinue();
        }

        private void btnAdditionChild_Click(object sender, RoutedEventArgs e)
        {
            if (NumAdult + NumChild < lstSeat.Count)
                txtNumChild.Text = (++NumChild).ToString();
            CheckBtnContinue();
        }

        private void btnSubtractionChild_Click(object sender, RoutedEventArgs e)
        {
            if (NumChild > 0)
                txtNumChild.Text = (--NumChild).ToString();
            CheckBtnContinue();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            lstAge = ageService.getListAge(NumAdult, NumChild);
            Continue?.Invoke(this, EventArgs.Empty); 
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Back?.Invoke(this, EventArgs.Empty);
        }

        private void CheckBtnContinue()
        {
            if (NumAdult + NumChild == lstSeat.Count)
                btnContinue.IsEnabled = true;
            else
                btnContinue.IsEnabled = false;
        }
    }
}
