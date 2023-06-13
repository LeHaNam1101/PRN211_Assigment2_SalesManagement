using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ProductObject
    {
        private int _productId;

        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }
        private int _categoryId;

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }
        private String productName;

        public String ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private String _weight;

        public String Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }
        private int _unitslnStock;

        public int UnitslnStock
        {
            get { return _unitslnStock; }
            set { _unitslnStock = value; }
        }
        public ProductObject()
        {

        }
        public ProductObject(int productId,int categoryId, String productName,String weight, decimal unitPrice, int unitslnStock)
        {
            ProductId = productId;
            CategoryId = categoryId;
            ProductName = productName;
            Weight = Weight;
            UnitPrice = unitPrice;
            UnitslnStock = unitslnStock;
        }
    }
}
