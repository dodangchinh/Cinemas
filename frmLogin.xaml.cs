using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        AccountService accountService;
        public frmLogin()
        {
            InitializeComponent();
            Validator.CheckDate();
            accountService = new AccountService();
            this.Show();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account();
            account.Username = txtUsername.Text;
            account.Password = txtPassword.Password;

            if(accountService.isExit(account))
            {
                account = accountService.getAccount(account.Username, account.Password);
                switch (account.Role)
                {
                    case "Admin":
                        frmAdmin frmAdmin = new frmAdmin(account);
                        this.Hide();
                        frmAdmin.ShowDialog();
                        break;
                    case "User":
                        frmUser frmUser = new frmUser(account);
                        this.Hide();
                        frmUser.ShowDialog();
                        break;
                    default:
                        break;
                }              
                this.Show();
            }
            else
                MessageBox.Show("Incorrect account!");

            txtUsername.Clear();
            txtPassword.Clear();
        }
    }
}
