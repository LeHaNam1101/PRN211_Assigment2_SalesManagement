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
    public partial class frmProduct : Form
    {
        int ProductId = 0;
        String type = "";
        IProductRepository productRepository = new ProductRepository();
        IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        BindingSource source;
        public frmProduct()
        {
            InitializeComponent();
        }

        public frmProduct(String accountType)
        {
            type = accountType;
            InitializeComponent();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            
            loadProduct();
        }

        private void loadProduct()
        {
            var listProduct = productRepository.GetProducts();
            try
            {
                source = new BindingSource();
                source.DataSource = listProduct;
                dataProduct.DataSource = listProduct;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load product list");
                throw;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            int id = 0;
            String name = "";
            int cateId = 0;
            String weight = "";
            decimal unitPrice = 0;
            int stock = 0;
            try
            {
                id = Convert.ToInt32(txtProductId.Text);
                name = txtProductName.Text;
                cateId = Convert.ToInt32(txtCategoryId.Text);
                weight = txtWeight.Text;
                unitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                stock = Convert.ToInt32(txtStock.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid information!");
                return;
            }
            ProductObject product = new ProductObject();
            product.ProductId = id;
            product.ProductName = name;
            product.CategoryId = cateId;
            product.UnitPrice = unitPrice;
            product.UnitslnStock = stock;
            product.Weight = weight;

            if(productRepository.GetProductById(product.ProductId) == null)
            {
                productRepository.insertProduct(product);
                loadProduct();
            }
            else
            {
                MessageBox.Show("This Product alreadry exit!");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure about that?", "Confirm", MessageBoxButtons.YesNo)==DialogResult.No)
            {
                return;
            }
            try
            {
                int id = Convert.ToInt32(txtProductId.Text);
                for(int i = 0; i<productRepository.GetIdProductFromOrderDetail().Count; i++)
                {
                    if(id == productRepository.GetIdProductFromOrderDetail()[i])
                    {
                        if (MessageBox.Show(this, "This product is exists in order detail, Are you want delete all?", "Confirm", MessageBoxButtons.YesNo)==DialogResult.No)
                        {
                            return;
                        }
                        orderDetailRepository.deleteOrderDetailById(id);
                        MessageBox.Show("This product was delete succesfull!!");
                    }
                }
                productRepository.deleteProduct(id);
                txtProductId.Text = "";
                txtCategoryId.Text = "";
                txtWeight.Text = "";
                txtProductName.Text = "";
                txtStock.Text = "";
                txtUnitPrice.Text = "";
                loadProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete false!!"+ ex.Message);
            }
        }

        private void dataProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            ProductId = Convert.ToInt32(dataProduct.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
            txtProductId.Text = dataProduct.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            txtCategoryId.Text = dataProduct.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            txtProductName.Text = dataProduct.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            txtWeight.Text = dataProduct.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            txtUnitPrice.Text = dataProduct.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
            txtStock.Text = dataProduct.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                String name = txtProductName.Text;
                int cateId = Convert.ToInt32(txtCategoryId.Text);
                String weight = txtWeight.Text;
                decimal unitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                int stock = Convert.ToInt32(txtStock.Text);

                ProductObject product = new ProductObject();
                product.ProductId = ProductId;
                product.ProductName = name;
                product.CategoryId = cateId;
                product.UnitPrice = unitPrice;
                product.UnitslnStock = stock;
                product.Weight = weight;

                productRepository.updateProduct(product);
                txtProductId.Text = "";
                txtCategoryId.Text = "";
                txtWeight.Text = "";
                txtProductName.Text = "";
                txtStock.Text = "";
                txtUnitPrice.Text = "";
                loadProduct();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid information!!!");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String txt = txtSearch.Text;
            var listProduct = productRepository.SearchProduct(txt);
            try
            {
                source = new BindingSource();
                source.DataSource = listProduct;
                dataProduct.DataSource = listProduct;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search Product list");
                throw;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            frmMain frmMain = new frmMain(type);
            frmMain.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtProductId.Text = "";
            txtCategoryId.Text = "";
            txtWeight.Text = "";
            txtProductName.Text = "";
            txtStock.Text = "";
            txtUnitPrice.Text = "";
            loadProduct();
            txtProductId.Enabled = true;
        }
    }
}
