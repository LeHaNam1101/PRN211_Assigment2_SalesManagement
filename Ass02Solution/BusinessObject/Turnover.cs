using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Turnover
    {
        private int _orderId;

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        private double _tunover;

        public double Tunover
        {
            get { return _tunover; }
            set { _tunover = value; }
        }

        private DateTime _ShippedDate;

        public DateTime ShippedDate
        {
            get { return _ShippedDate; }
            set { _ShippedDate = value; }
        }

        public Turnover()
        {

        }
        public Turnover(int oderId, double tunover,DateTime shippedDate)
        {
            OrderId = oderId;
            Tunover = tunover;
            ShippedDate = shippedDate;
        }
    }
}
