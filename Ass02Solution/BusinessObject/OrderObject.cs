using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OrderObject
    {
        private int _orderId;

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }
        private int _memberId;

        public int MemberId
        {
            get { return _memberId; }
            set { _memberId = value; }
        }
        private DateTime _orderDate;

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }
        private DateTime _requiredDate;

        public DateTime RequiredDate
        {
            get { return _requiredDate; }
            set { _requiredDate = value; }
        }
        private DateTime _shippedDate;

        public DateTime ShippedDate
        {
            get { return _shippedDate; }
            set { _shippedDate = value; }
        }
        private decimal _freight;

        public decimal Freight
        {
            get { return _freight; }
            set { _freight = value; }
        }

        public OrderObject()
        {

        }
        public OrderObject(int orderId,int memberId, DateTime orederDate, DateTime requiredDate,DateTime shippedDate,decimal freight)
        {
            OrderId = orderId;
            MemberId = memberId;
            OrderDate = orederDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            Freight = freight;
        }
    }
}
