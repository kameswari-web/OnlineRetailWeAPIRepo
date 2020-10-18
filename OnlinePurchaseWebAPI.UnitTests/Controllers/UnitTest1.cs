using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlinePurchaseWebAPI.Controllers;
using OnlinePurchaseWebAPI.Models;
using System.Data.Entity;

namespace OnlinePurchaseWebAPI.UnitTests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void OrderItemUnitTest()
        {
            Online_Products_PurchaseEntities1 db = new Online_Products_PurchaseEntities1();
            var controller = new ProductsController();

            controller.Request = new HttpRequestMessage();
            //{
            //    RequestUri = new Uri("http://localhost/api/products")
            //};
            controller.Configuration = new HttpConfiguration();
            //controller.Configuration.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional });

            //controller.RequestContext.RouteData = new HttpRouteData(
            //    route: new HttpRoute(),
            //    values: new HttpRouteValueDictionary { { "controller", "products" } });

            // Act
            //Products product;
            var response = controller.GetProducts(1) as OkNegotiatedContentResult<Products>;
            //
            var testProducts = GetTestProducts();

            //HttpResponseMessage response = controller.PostProducts(product);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(testProducts[1].ProductName, response.Content.ProductName);
        }

        private List<Products> GetTestProducts()
        {
            var testProducts = new List<Products>();
            testProducts.Add(new Products { ProductId = 1, ProductName = "Demo1", Price = 100, TotalQuantity = 10 });
            testProducts.Add(new Products { ProductId = 2, ProductName = "Demo2", Price = 150.0, TotalQuantity = 12 });
            testProducts.Add(new Products { ProductId = 3, ProductName = "Demo3", Price = 500, TotalQuantity = 25 });
            testProducts.Add(new Products { ProductId = 4, ProductName = "Demo4", Price = 55, TotalQuantity = 105 });

            return testProducts;
        }

    }
}
