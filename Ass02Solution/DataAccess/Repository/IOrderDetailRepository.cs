using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetailObject> GetOrderDetails();
        OrderDetailObject GetOrderDetailById(int id);
        void updateOrderDetail(OrderDetailObject orderDetail);
        void deleteOrderDetailById(int id);
        void insertOrderDetail(OrderDetailObject orderDetail);
        void deleteOrderDetailByOrderId(int id);
    }
}
