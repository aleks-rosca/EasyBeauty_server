using Dapper;
using EasyBeauty_server.Controllers;
using EasyBeauty_server.DataAccess;
using EasyBeauty_server.Models;
using EasyBeauty_server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace easybeauty_server_tests
{
    public class ServiceControllerTest
    {
        readonly ServiceController controller;

        public ServiceControllerTest()
        {
            controller = new ServiceController();
        }
        [Fact]
        public List<Service> GetAllServices()
        {
            var result = controller.GetServices();
            Assert.IsType<List<Service>>(result);
            return null;

        }

        [Fact]
        public void CreateService()
        {
            using (DBConnection.GetConnection())
            {
                var s = new Service
            {
                Name = "from xTest",
                Description = "some tests",
                Price = 220,
                Image = "",
                Duration = 30
            };

                controller.CreateService(s);
                List<Service> list = GetAllServices();
                foreach (var l in list)
                {
                    if (l.Name.Equals(s.Name))
                    {
                        Assert.Equal(l.Name, s.Name);
                    }
                }
            }
            
        }

    }
}
