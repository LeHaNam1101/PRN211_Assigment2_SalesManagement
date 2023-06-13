using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<OrderObject> getOrders() => OrderDAO.Instance.getListOrder();

        public OrderObject GetOrderById(int id) => OrderDAO.Instance.getOrderById(id);

        public void updateOrder(OrderObject order) => OrderDAO.Instance.updateOrder(order);

        public void deleteOrderById(int id) => OrderDAO.Instance.deleteOrder(id);

        public void insertOrder(OrderObject order) => OrderDAO.Instance.addNewOrder(order);

        public IEnumerable<Turnover> GetTurnovers(DateTime startDate, DateTime endDate) => OrderDAO.Instance.getListTur(startDate,endDate);

        public IEnumerable<OrderObject> getOrdersByUserId(MemberObject member) => OrderDAO.Instance.getListOrderByUserId(member);

        public List<int> GetIdOrderFromOrderDetail() => OrderDAO.Instance.checkOrderInOrderDetail();
    }
}
