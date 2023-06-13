using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using System.Data.SqlClient;
namespace DataAccess
{
    public class OrderDetailDAO : BaseDAL
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instancelock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetailObject> getListOrderDetail()
        {
            IDataReader dataReader = null;
            String sql = "select * from [OrderDetail]";
            var orderDetailList = new List<OrderDetailObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(sql, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    orderDetailList.Add(new OrderDetailObject
                    {
                        OrderId = dataReader.GetInt32(0),
                        ProductId = dataReader.GetInt32(1),
                        UnitPrice = dataReader.GetDecimal(2),
                        Quantity = dataReader.GetInt32(3),
                        Discount = dataReader.GetDouble(4)

                    });
                }
                return orderDetailList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public OrderDetailObject getOrderDetailById(int id)
        {
            OrderDetailObject orderDetail = null;
            IDataReader reader = null;
            String sql = "Select * from [OrderDetail] where OrderId = @OrderDetailId";
            try
            {
                var param = dataProvider.CreateParameter("OrderDetailId", 4, id, DbType.Int32);
                reader = dataProvider.GetDataReader(sql, CommandType.Text, out connection, param);
                if (reader.Read())
                {
                    orderDetail = new OrderDetailObject
                    {
                        OrderId = reader.GetInt32(0),
                        ProductId = reader.GetInt32(1),
                        UnitPrice = reader.GetDecimal(2),
                        Quantity = reader.GetInt32(3),
                        Discount = reader.GetDouble(4)
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return orderDetail;
        }

        public void addNewOrderDetail(OrderDetailObject orderDetail)
        {
            try
            {
                    String sql = "Insert OrderDetail values(@OrderId,@ProductId,@UnitPrice,@Quantity,@Discount)";
                    var para = new List<SqlParameter>();
                    para.Add(dataProvider.CreateParameter("@OrderId", 4, orderDetail.OrderId, DbType.Int32));
                    para.Add(dataProvider.CreateParameter("@ProductId", 4, orderDetail.ProductId, DbType.Int32));
                    para.Add(dataProvider.CreateParameter("@UnitPrice", 4, orderDetail.UnitPrice, DbType.Decimal));
                    para.Add(dataProvider.CreateParameter("@Quantity", 4, orderDetail.Quantity, DbType.Int32));
                    para.Add(dataProvider.CreateParameter("@Discount", 4, orderDetail.Discount, DbType.Double));
                    dataProvider.Insert(sql, CommandType.Text, para.ToArray());
                
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

        public void deleteOrderDetail(int id)
        {
            try
            {

                OrderDetailObject order = getOrderDetailById(id);
                String sql = "Delete from [OrderDetail] where ProductId = @OrderId";
                var param = dataProvider.CreateParameter("@OrderId", 4, id, DbType.Int32);
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

        public void deleteOrderDetailByOrderId(int id)
        {
            try
            {

                OrderDetailObject order = getOrderDetailById(id);
                String sql = "Delete from [OrderDetail] where OrderId = @OrderId";
                var param = dataProvider.CreateParameter("@OrderId", 4, id, DbType.Int32);
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

        public void updateOrder(OrderDetailObject orderDetail)
        {
            try
            {
                String sql = "UPDATE [dbo].[OrderDetail] SET[OrderId] = '" + orderDetail.OrderId + "' ," +
                    "[ProductId] = '" + orderDetail.ProductId + "',[UnitPrice] = '" + orderDetail.UnitPrice + "'," +
                    "[Quantity] = '" + orderDetail.Quantity +"'";
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

    }
}

