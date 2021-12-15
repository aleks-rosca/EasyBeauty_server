namespace EasyBeauty_server.Repository
{
    using Dapper;
    using DataAccess;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class ServiceRepo
    {
        public static void CreateService(Service service)
        {
            DBConnection.DatabaseConnection.Execute(@"INSERT INTO Service (name, description, price, image, duration) VALUES (@name, @description, @price, @image, @duration)",
            new { name = service.Name, description = service.Description, price = service.Price, image = service.Image, duration = service.Duration });
        }
        public static bool CheckServiceName(string name)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Service WHERE name = @name", new { name });
        }
        public static void DeleteService(int id)
        {
            DBConnection.DatabaseConnection.Execute(@"DELETE FROM Service WHERE ID = @id;", new { id });
        }
        public static void EditService(int id, Service service)
        {
            DBConnection.DatabaseConnection.Execute(@"UPDATE Service SET name = @name, description = @description, price = @price, image = @image, duration = @duration WHERE ID = @id",
                new { id, name = service.Name, description = service.Description, price = service.Price, image = service.Image, duration = service.Duration });
        }
        public static List<Service> GetServices()
        {
            return DBConnection.DatabaseConnection.Query<Service>("Select * from Service").ToList();
        }
    }
}
