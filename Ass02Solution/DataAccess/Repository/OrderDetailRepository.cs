using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void deleteOrderDetailById(int id) => OrderDetailDAO.Instance.deleteOrderDetail(id);

        public void deleteOrderDetailByOrderId(int id) => OrderDetailDAO.Instance.deleteOrderDetailByOrderId(id);

        public OrderDetailObject GetOrderDetailById(int id) => OrderDetailDAO.Instance.getOrderDetailById(id);

        public IEnumerable<OrderDetailObject> GetOrderDetails() => OrderDetailDAO.Instance.getListOrderDetail();
        public void insertOrderDetail(OrderDetailObject orderDetail) => OrderDetailDAO.Instance.addNewOrderDetail(orderDetail);

        public void updateOrderDetail(OrderDetailObject orderDetail) => OrderDetailDAO.Instance.updateOrder(orderDetail);

    }
}
