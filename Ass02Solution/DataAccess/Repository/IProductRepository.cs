using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<ProductObject> GetProducts();
        void insertProduct(ProductObject product);
        ProductObject GetProductById(int id);
        void deleteProduct(int id);
        void updateProduct(ProductObject product);
        List<int> GetIdProductFromOrderDetail();
        IEnumerable<ProductObject> SearchProduct(String txt);
    }
}
