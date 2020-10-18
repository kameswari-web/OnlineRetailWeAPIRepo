using OnlinePurchaseWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderItemDataAccess;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Http.Description;


namespace OnlinePurchaseWebAPI.Controllers
{
    public class OrdersController : ApiController
    {

        // GET: api/Orders
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Orders/5
        [HttpGet]
        [ResponseType(typeof(OrderItems))]
        public IHttpActionResult GetOrdersbyOrderId(int id)
        {
            using (Online_Products_PurchaseEntitiesDB db = new Online_Products_PurchaseEntitiesDB())
            {
                
                var Items = db.OrderItems.FirstOrDefault(i => i.OrderId == id);

                if (Items == null)
                {
                    return Content(HttpStatusCode.NotFound, "Order Not Found");
                }
                else
                {
                    return Ok(Items);
                }
            }
        }

        // GET: api/Orders/Sam
        //[HttpGet]
        //public IHttpActionResult GetOrdersbyCustomerName(string name)
        //{
        //    using (Online_Products_PurchaseEntitiesDB db = new Online_Products_PurchaseEntitiesDB())
        //    {
        //        var OrderItems = db.OrderItems.FirstOrDefault(i => i.CustomerName == name);

        //        if (OrderItems == null)
        //        {
        //            return Content(HttpStatusCode.NotFound, "Order Not Found for the " + name);
        //        }

        //        return Ok(OrderItems);
        //    }
        //}

        // POST: api/Orders
        [HttpPost]
        [ActionName("JSONMethod")]
        public HttpResponseMessage Post([FromBody] OrderItemDetails itemDetails)
        {
            try
            {
                //byte[] body = ObjectToByteArray(value);
                //string output = Encoding.UTF8.GetString(body);

                //OrderItemDetails itemDetails = (OrderItemDetails)JsonConvert.DeserializeObject(output, typeof(OrderItemDetails));
                using (Online_Products_PurchaseEntitiesDB db = new Online_Products_PurchaseEntitiesDB())
                {

                    db.sp_Add_OrderItemDetails(itemDetails.Customers.CustomerName, itemDetails.Customers.EmailId, itemDetails.Customers.Address, itemDetails.Customers.PhoneNo, itemDetails.Orders.OrderStatus, itemDetails.Products.ProductName, itemDetails.Products.OrderQuantity);
                    db.SaveChanges();
                                        
                    var message = Request.CreateResponse(HttpStatusCode.Created, itemDetails);
                    return message;

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(OrderItemDetails))]
        [HttpPut]
        [Route("api/UpdateOrderQuantity/{id}/{prodId}")]
        public HttpResponseMessage UpdateOrderQuantity([FromUri]int id, int prodId, [FromBody]OrderItemDetails items)
        {
            try
            {
                using (Online_Products_PurchaseEntitiesDB db = new Online_Products_PurchaseEntitiesDB())
                {
                    var entity = db.OrderItems.FirstOrDefault(i => i.OrderId == id && i.ProductId == prodId);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order with ProductId " + prodId + " Not Found");
                    }
                    else
                    {
                        db.sp_Update_OrderItemsQuantity(id, prodId, items.Products.OrderQuantity);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(OrderItemDetails))]
        [HttpDelete]
        [Route("api/CancelOrder/{id}")]
        public HttpResponseMessage CancelOrderItem(int id)
        {
            try
            {
                using (Online_Products_PurchaseEntitiesDB db = new Online_Products_PurchaseEntitiesDB())
                {
                    var entity = db.OrderItems.FirstOrDefault(i => i.OrderId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order Not found");
                    }
                    else
                    {
                        db.sp_Cancel_OrderItems(id);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
