using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<OrderObject> getOrders();
        OrderObject GetOrderById(int id);
        void updateOrder(OrderObject order);
        void deleteOrderById(int id);
        void insertOrder(OrderObject order);

        IEnumerable<Turnover> GetTurnovers(DateTime startDate,DateTime endDate);
        List<int> GetIdOrderFromOrderDetail();
        IEnumerable<OrderObject> getOrdersByUserId(MemberObject member);
    }
}
