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
    public class OrderDAO : BaseDAL
    {
        private static OrderDAO instance = null;
        private static readonly object instancelock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderObject> getListOrder()
        {
            IDataReader dataReader = null;
            String sql = "select * from [Order]";
            var orderList = new List<OrderObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(sql, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    orderList.Add(new OrderObject
                    {
                        OrderId = dataReader.GetInt32(0),
                        MemberId = dataReader.GetInt32(1),
                        OrderDate = dataReader.GetDateTime(2),
                        RequiredDate = dataReader.GetDateTime(3),
                        ShippedDate = dataReader.GetDateTime(4),
                        Freight = dataReader.GetDecimal(5),
                    });
                }
                return orderList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<OrderObject> getListOrderByUserId(MemberObject member)
        {
            IDataReader dataReader = null;
            String sql = "select * from [Order] where MemberId = '"+member.MemberId+"'";
            var orderList = new List<OrderObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(sql, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    orderList.Add(new OrderObject
                    {
                        OrderId = dataReader.GetInt32(0),
                        MemberId = dataReader.GetInt32(1),
                        OrderDate = dataReader.GetDateTime(2),
                        RequiredDate = dataReader.GetDateTime(3),
                        ShippedDate = dataReader.GetDateTime(4),
                        Freight = dataReader.GetDecimal(5),
                    });
                }
                return orderList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public OrderObject getOrderById(int id)
        {
            OrderObject order = null;
            IDataReader reader = null;
            String sql = "Select * from [Order] where OrderId = @OrderId";
            try
            {
                var param = dataProvider.CreateParameter("OrderId", 4, id, DbType.Int32);
                reader = dataProvider.GetDataReader(sql, CommandType.Text, out connection, param);
                if (reader.Read())
                {
                    order = new OrderObject
                    {
                        OrderId = reader.GetInt32(0),
                        MemberId= reader.GetInt32(1),
                        OrderDate= reader.GetDateTime(2),
                        RequiredDate= reader.GetDateTime(3),
                        ShippedDate= reader.GetDateTime(4),
                        Freight = reader.GetDecimal(5),
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return order;
        }

        public void addNewOrder(OrderObject order)
        {
            try
            {
                OrderObject o = getOrderById(order.OrderId);
                if (o == null)
                {
                    String sql = "Insert [Order] values(@OrderId,@MemberId,@OrderDate,@RequiredDate,@ShippedDate,@Freight)";
                    var para = new List<SqlParameter>();
                    para.Add(dataProvider.CreateParameter("@OrderId", 4, order.OrderId, DbType.Int32));
                    para.Add(dataProvider.CreateParameter("@MemberId", 4, order.MemberId, DbType.Int32));
                    para.Add(dataProvider.CreateParameter("@OrderDate", 50, order.OrderDate, DbType.DateTime));
                    para.Add(dataProvider.CreateParameter("@RequiredDate", 50, order.RequiredDate, DbType.DateTime));
                    para.Add(dataProvider.CreateParameter("@ShippedDate", 50, order.ShippedDate, DbType.DateTime));
                    para.Add(dataProvider.CreateParameter("@Freight", 4, order.Freight, DbType.Decimal));
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

        public void deleteOrder(int id)
        {
            try
            {
                OrderObject order = getOrderById(id);
                String sql = "Delete from [Order] where OrderId = @OrderId";
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

        public void updateOrder(OrderObject order)
        {
            try
            {
                String sql = "UPDATE [dbo].[Order] SET[OrderId] = '"+order.OrderId+"' ," +
                    "[MemberId] = '"+order.MemberId+"',[OrderDate] = '"+order.OrderDate+"'," +
                    "[RequiredDate] = '"+order.RequiredDate+"',[ShippedDate] = '"+order.ShippedDate+"'," +
                    "[Freight] = '"+order.Freight+"' WHERE[OrderId] = '"+order.OrderId+"'";
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

        public IEnumerable<Turnover> getListTur(DateTime starDate,DateTime endDate)
        {
            IDataReader dataReader = null;
            String sql = "select o.OrderId,SUM(od.UnitPrice*od.Quantity - od.UnitPrice*(od.Discount/100)) " +
                "as Turnover,o.ShippedDate from [Order] o join OrderDetail od on o.OrderId = od.OrderId " +
                "where ShippedDate Between '"+starDate+"' and '"+endDate+"' group by o.OrderId,o.ShippedDate " +
                "order by Turnover DESC";
            var listTur = new List<Turnover>();
            try
            {
                dataReader = dataProvider.GetDataReader(sql, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    listTur.Add(new Turnover
                    {
                        OrderId = dataReader.GetInt32(0),
                        Tunover = dataReader.GetDouble(1),
                        ShippedDate = dataReader.GetDateTime(2),
                    });
                }
                return listTur;
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

        public List<int> checkOrderInOrderDetail()
        {
            IDataReader dataReader = null;
            String sql = "select distinct orderid from orderdetail";
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
