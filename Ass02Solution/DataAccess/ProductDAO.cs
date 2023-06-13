using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BusinessObject;


namespace DataAccess
{
    public class ProductDAO : BaseDAL
    {
        private static ProductDAO instance = null;
        private static readonly object instancelock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get 
            {
                lock (instancelock)
                {
                    if(instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<ProductObject> getListProduct()
        {
            IDataReader dataReader = null;
            String sql = "select * from Product";
            var productList = new List<ProductObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(sql, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    productList.Add(new ProductObject
                    {
                        ProductId = dataReader.GetInt32(0),
                        ProductName = dataReader.GetString(2),
                        CategoryId = dataReader.GetInt32(1),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitslnStock = dataReader.GetInt32(5)
                    });
                }
                return productList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ProductObject getProductById(int id)
        {
            ProductObject product = null;
            IDataReader reader = null;
            String sql = "Select * from Product where ProductId = @ProductId";
            try
            {
                var param = dataProvider.CreateParameter("ProductId", 4, id, DbType.Int32);
                reader = dataProvider.GetDataReader(sql,CommandType.Text,out connection,param);
                if(reader.Read())
                {
                    product = new ProductObject
                    {
                        ProductId = reader.GetInt32(0),
                        CategoryId=reader.GetInt32(1),
                        ProductName=reader.GetString(2),
                        Weight=reader.GetString(3),
                        UnitPrice=reader.GetDecimal(4),
                        UnitslnStock = reader.GetInt32(5),
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return product;
        }
        public void addNewProduct(ProductObject product)
        {
            try
            {
                    ProductObject p = getProductById(product.ProductId);
                if(p == null)
                {
                    String sql = "Insert Product values(@ProductId,@CategoryId,@ProductName,@Weight,@UnitPrice,@UnitslnStock)";
                    var para = new List<SqlParameter>();
                    para.Add(dataProvider.CreateParameter("@ProductId", 4, product.ProductId, DbType.Int32));
                    para.Add(dataProvider.CreateParameter("@CategoryId", 4, product.CategoryId, DbType.Int32));
                    para.Add(dataProvider.CreateParameter("@ProductName", 50, product.ProductName, DbType.String));
                    para.Add(dataProvider.CreateParameter("@Weight", 50, product.Weight, DbType.String));
                    para.Add(dataProvider.CreateParameter("@UnitPrice", 50, product.UnitPrice, DbType.Decimal));
                    para.Add(dataProvider.CreateParameter("@UnitslnStock", 4, product.UnitslnStock, DbType.Int32));
                    dataProvider.Insert(sql, CommandType.Text, para.ToArray());
                }
                else
                {
                    throw new Exception("The product already exit.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            
        }

        public void deleteProduct(int id)
        {
            try
            {
                ProductObject product = getProductById(id);
                String sql = "Delete from Product where ProductId = @ProductId";
                var param = dataProvider.CreateParameter("@ProductId", 4, id, DbType.Int32);
                dataProvider.Delete(sql, CommandType.Text, param);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void updateProduct(ProductObject product)
        {
            try
            {
                    String sql = "Update Product set ProductId = '"+product.ProductId+"',CategoryId = '"+product.CategoryId+"'," +
                        "ProductName = '"+product.ProductName+"',[Weight] = '"+product.Weight+"'," +
                        "UnitPrice = '"+product.UnitPrice+"',UnitslnStock = '"+product.UnitslnStock+"'" +
                        "where ProductId='"+product.ProductId+"'";
                    var para = new List<SqlParameter>();
                    dataProvider.Update(sql, CommandType.Text, para.ToArray());
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public IEnumerable<ProductObject> searchProduct(String txt)
        {
            IDataReader dataReader = null;
            int txt_int = 0;
            decimal txtde = 0;
            double txtdou = 0;
            try
            {
                txt_int = Convert.ToInt32(txt);
                txtde = Convert.ToDecimal(txt);
                txtdou = Convert.ToDouble(txt);
            }
            catch (Exception)
            {
            }
            String sql = "select * from Product where ProductId like '%"+txt_int+"%' or ProductName like '%"+txt+"%' or UnitPrice like '%"+txtde+"%' or UnitslnStock like '%"+txtdou+"%'";
            var productList = new List<ProductObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(sql, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    productList.Add(new ProductObject
                    {
                        ProductId = dataReader.GetInt32(0),
                        ProductName = dataReader.GetString(2),
                        CategoryId = dataReader.GetInt32(1),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitslnStock = dataReader.GetInt32(5)
                    });
                }
                return productList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<int> checkProductInOrderDetail()
        {
            IDataReader dataReader = null;
            String sql = "select distinct ProductId from OrderDetail";
            var listTur = new List<int>();
            try
            {
                dataReader = dataProvider.GetDataReader(sql, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    listTur.Add(dataReader.GetInt32(0));
                }
                return listTur;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
