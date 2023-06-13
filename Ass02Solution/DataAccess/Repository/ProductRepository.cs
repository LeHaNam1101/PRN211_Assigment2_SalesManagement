using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void deleteProduct(int id) => ProductDAO.Instance.deleteProduct(id);

        public List<int> GetIdProductFromOrderDetail() => ProductDAO.Instance.checkProductInOrderDetail();

        public ProductObject GetProductById(int id) => ProductDAO.Instance.getProductById(id);

        public IEnumerable<ProductObject> GetProducts() => ProductDAO.Instance.getListProduct();

        public void insertProduct(ProductObject product) => ProductDAO.Instance.addNewProduct(product);

        public IEnumerable<ProductObject> SearchProduct(string txt) => ProductDAO.Instance.searchProduct(txt);
        public void updateProduct(ProductObject product) => ProductDAO.Instance.updateProduct(product);
    }
}
