using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessObject;
using DataAccess;
using DataAccess.Repository;
namespace SaleWinApp
{
    public partial class frmOrder : Form
    {
        String type = "";
        int OrderId = 0;
        IOrderRepository orderRepository = new OrderRepository();
        IMemberRepository memberRepository = new MemberRepository();
        IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        BindingSource source;
        public frmOrder()
        {
            InitializeComponent();
        }
        public String account = "";
        public String accountType = "";
        public frmOrder(string accountType)
        {
            type = accountType;
            InitializeComponent();
            if (!accountType.Equals("admin@fstore.com"))
            {
                btnAddNew.Hide();
                btnDelete.Hide();
                btnStatistics.Hide();
                btnUpdate.Hide();
                btnReset.Hide();

            }
        }
        private void frmOrder_Load(object sender, EventArgs e)
        {
            
            if(type == "admin@fstore.com")
            {
                loadOrder();
            }
            else
            {
                loadOrderOfUser();
            }
            
        }

        private void loadOrderOfUser()
        {
            MemberObject member = null;
            foreach (var item in memberRepository.getListAccount(type))
            {
                member = item;
            }
            var listOrder = orderRepository.getOrdersByUserId(member);
            try
            {
                source = new BindingSource();
                source.DataSource = listOrder;
                dataOrder.DataSource = listOrder;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Order list");
                throw;
            }
        }

        private void loadOrder()
        {
            var listOrder = orderRepository.getOrders();
            try
            {
                source = new BindingSource();
                source.DataSource = listOrder;
                dataOrder.DataSource = listOrder;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Order list");
                throw;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            int OrderId = 0;
            int MemberId = 0;
            DateTime OrderDate ;
            DateTime RequiredDate ;
            DateTime ShippedDate ;
            decimal Freight = 0;
            try
            {
                OrderId = Convert.ToInt32(txtOrderID.Text);
                MemberId = Convert.ToInt32(txtMemberID.Text);
                OrderDate = Convert.ToDateTime(dateOrder.Text);
                RequiredDate = Convert.ToDateTime(dateRequired.Text);
                ShippedDate = Convert.ToDateTime(dateShipped.Text);
                Freight = Convert.ToDecimal(txtFreight.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid information!");
                return;
            }

            OrderObject order = new OrderObject();
            order.OrderId = OrderId;
            order.MemberId = MemberId;
            order.OrderDate = OrderDate;
            order.RequiredDate = RequiredDate;
            order.ShippedDate = ShippedDate;
            order.Freight = Freight;

            orderRepository.insertOrder(order);
            loadOrder();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you want to delete this order?", "Confirm", MessageBoxButtons.YesNo)==DialogResult.No)
            {
                return;
            }
            try
            {
                int orderId = Convert.ToInt32(txtOrderID.Text);

                for(int i = 0; i<orderRepository.GetIdOrderFromOrderDetail().Count; i++)
                {
                    if(orderId == orderRepository.GetIdOrderFromOrderDetail()[i])
                    {
                        if (MessageBox.Show(this, "This order is exists in order detail, Are you want delete all?", "Confirm", MessageBoxButtons.YesNo)==DialogResult.No)
                        {
                            return;
                        }
                        orderDetailRepository.deleteOrderDetailByOrderId(orderId);
                        MessageBox.Show("This order was delete succesfull!!");
                    }
                }
                orderRepository.deleteOrderById(OrderId);
                txtOrderID.Text = "";
                txtMemberID.Text = "";
                dateOrder.Text = "";
                dateRequired.Text = "";
                dateShipped.Text = "";
                txtFreight.Text = "";
                loadOrder();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void dataOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOrderID.Enabled =  false;
            OrderId = Convert.ToInt32(dataOrder.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
            txtOrderID.Text = dataOrder.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            txtMemberID.Text = dataOrder.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            dateOrder.Text = dataOrder.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            dateRequired.Text = dataOrder.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            dateShipped.Text = dataOrder.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
            txtFreight.Text = dataOrder.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int MemberId = 0;
                DateTime OrderDate;
                DateTime RequiredDate;
                DateTime ShippedDate;
                decimal Freight = 0;
                try
                {
                    MemberId = Convert.ToInt32(txtMemberID.Text);
                    OrderDate = Convert.ToDateTime(dateOrder.Text);
                    RequiredDate = Convert.ToDateTime(dateRequired.Text);
                    ShippedDate = Convert.ToDateTime(dateShipped.Text);
                    Freight = Convert.ToDecimal(txtFreight.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid information!");
                    return;
                }

                OrderObject order = new OrderObject();
                order.OrderId = OrderId;
                order.MemberId = MemberId;
                order.OrderDate = OrderDate;
                order.RequiredDate = RequiredDate;
                order.ShippedDate = ShippedDate;
                order.Freight = Freight;

                orderRepository.updateOrder(order);

                txtOrderID.Text = "";
                txtMemberID.Text = "";
                dateOrder.Text = "";
                dateRequired.Text = "";
                dateShipped.Text = "";
                txtFreight.Text = "";

                loadOrder();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            DateTime starDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime endDate = Convert.ToDateTime(txtEnddate.Text);

            var listTur = orderRepository.GetTurnovers(starDate,endDate);
            try
            {
                source = new BindingSource();
                source.DataSource = listTur;
                dataOrder.DataSource = listTur;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Order list");
                throw;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtOrderID.Text = "";
            txtMemberID.Text = "";
            dateOrder.Text = "";
            dateRequired.Text = "";
            dateShipped.Text = "";
            txtFreight.Text = "";
            txtOrderID.Enabled = true;
            loadOrder();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            frmMain frmMain = new frmMain(type);
            frmMain.Show();
        }

        private void txtOrderID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
