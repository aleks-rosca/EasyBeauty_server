namespace EasyBeauty_server.Repository
{
    using Dapper;
    using DataAccess;
    using Models;
    using System.Collections.Generic;
    using System.Linq;



    public static class ProductRepo
{
    public static void CreateProduct(Product product)
    {
        DBConnection.DatabaseConnection.Execute(@"INSERT INTO Product (name, description, price, image) VALUES (@name, @description, @price, @image)",
        new { name = product.Name, description = product.Description, price = product.Price, image = product.Image });
    }

    public static bool CheckProductName(string name)
    {
        return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Product WHERE name = @name", new { name });
    }

    public static void DeleteProduct(int id)
    {
        DBConnection.DatabaseConnection.Execute(@"DELETE FROM Product WHERE ID = @id;", new { id });
    }

    public static void EditProduct(int id, Product product)
    {
        DBConnection.DatabaseConnection.Execute(@"UPDATE Product SET name = @name, description = @description, price = @price, image = @image WHERE ID = @id",
            new { id, name = product.Name, description = product.Description, price = product.Price, image = product.Image});
    }

    public static List<Product> GetProducts()
    {
        return DBConnection.DatabaseConnection.Query<Product>("Select * from Product").ToList();
    }
}
}
