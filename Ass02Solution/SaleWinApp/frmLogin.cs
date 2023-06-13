using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;

namespace SaleWinApp
{
    public partial class frmLogin : Form
    {
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.rememberMe.Equals("0"))
            {
                rememberMe.Checked = false;
            }
            if (Properties.Settings.Default.email != string.Empty)
            {
                rememberMe.Checked = true;
                txtEmail.Text = Properties.Settings.Default.email;
                txtPassword.Text = Properties.Settings.Default.password;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rememberMe.Checked)
            {
                Properties.Settings.Default.email = txtEmail.Text;
                Properties.Settings.Default.password = txtPassword.Text;
                Properties.Settings.Default.rememberMe = "1";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.rememberMe = "0";
                Properties.Settings.Default.email = "";
                Properties.Settings.Default.password = "";
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.email != string.Empty)
            {
                txtEmail.Text = Properties.Settings.Default.email;
                txtPassword.Text = Properties.Settings.Default.password;
            }
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Enter your username!");
                txtEmail.Select();
                return;

            }
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Enter your password!");
                txtPassword.Select();
                return;
            }
            var conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            string username = conf["User:Email"];
            string password = conf["User:Password"];


            String Email = txtEmail.Text;
            String Password = txtPassword.Text;
            MemberObject member = memberRepository.loginMember(Email, Password);
            if (Email.Equals(username) && Password.Equals(password))
            {
                frmMain f = new frmMain(username);
                f.Show();
                this.Hide();
            }
            else if(member != null)
            {
                String accountType = "";
                if (Email.Equals(username))
                {
                    accountType = "admin";
                }
                else
                {
                    accountType = "guess";
                }
                frmMain f = new frmMain(Email);
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("This account doesnt not exit!!!");
                return;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
