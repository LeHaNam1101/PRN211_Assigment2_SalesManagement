using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OrderDetailObject
    {
        private int _orderId;

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }
        private int _productId;

        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }
        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        private double _discount;

        public double Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }
        public OrderDetailObject()
        {

        }
        public OrderDetailObject(int orderId,int productId,decimal unitPrice, int quantity, double discount)
        {
            OrderId = orderId;
            ProductId = productId;
            UnitPrice = unitPrice; 
            Quantity = quantity;
            Discount = discount;
        }
    }
}
