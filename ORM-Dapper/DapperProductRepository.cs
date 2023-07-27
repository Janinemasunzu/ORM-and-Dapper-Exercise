using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    internal class DapperProductRepository : IProducstRepository

    {
        private readonly IDbConnection _connection;
      

        public DapperProductRepository(IDbConnection connection) 
        {
            _connection = connection;
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("insert into product (name, price, categoryID) values (@name, @price, @categoryID);",
                new { name = name, price = price, categoryID = categoryID });
        }

        public void DeleteProduct(int id)
        {
            _connection.Execute("delete from reviews where ProductID = @id", 
                new { id = id });
            _connection.Execute("delete from sales where ProductID = @id",
                new { id = id });

            _connection.Execute("delete from products where ProductID = @id",
                new { id = id });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("select * from products;");
        }

        public Product GetProduct(int id)
        {
            return _connection.QuerySingle<Product>("select * from products where productId = @id, new { id = id }");
        }

        public void UpdateProduct(Product product)
        {
            _connection.Execute("Update product" + "set name = @name," + "Price = @price," + "categoryID = @categoryId,"
                            + " onsaleLevel = @onsaleLevel" + "where ProductID = @id;", new
                            {
                                id = product.ProductID,
                                name = product.Name,
                                price = product.Price,
                                categoryID = product.categoryID,
                                onsale = product.onSale,
                                stockLevel = product.stockLevel
                            });
        }
    }
}
