using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaleWinApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        String type = "";
        
        public frmMain(String accountType)
        {
            type = accountType;
            InitializeComponent();
            if (!accountType.Equals("admin@fstore.com"))
            {
                btnProduct.Hide();
            }
        }
        private void btnAccount_Click(object sender, EventArgs e)
        {
            this.Close();
            frmMember frmMember = new frmMember(type);
            frmMember.Show();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            this.Close();
            frmOrder frmOrder = new frmOrder(type);
            frmOrder.Show();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            this.Close();
            frmProduct frmProduct = new frmProduct(type);
            frmProduct.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
        }
    }
}
