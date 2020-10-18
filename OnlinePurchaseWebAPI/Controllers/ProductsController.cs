using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using OnlinePurchaseWebAPI.Models;
using OrderItemDataAccess;

namespace OnlinePurchaseWebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private Online_Products_PurchaseEntities1 db = new Online_Products_PurchaseEntities1();

        
        // GET: api/Products
        public IEnumerable<Products> GetProducts()
        {
            List<Products> products = new List<Products>();

            
            return db.Products.ToList();
        }

        // GET: api/Products/5
        [ResponseType(typeof(Products))]
        public IHttpActionResult GetProducts(int id)
        {
            var product = db.Products.Find(id);

            if (product == null)
            {
                return Content(HttpStatusCode.NotFound, "Requested Product Not Found");
            }
            else
            {
                return Ok(product);
            }

            //return product;
        }

        // PUT: api/Products/5

        [ResponseType(typeof(HttpResponseMessage))]
        [HttpPut]
        [Route("api/UpdateProduct/{id}")]
        public HttpResponseMessage UpdateProduct(int id, [FromBody]Products products)
        {
           try
            {
                var entity = db.Products.FirstOrDefault(i => i.ProductId == id);

                if(entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id " + id + " Not Found");
                }
                else
                {
                    entity.ProductName = products.ProductName;
                    entity.TotalQuantity = products.TotalQuantity;
                    entity.Price = products.Price;

                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }

            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST: api/Products
        [ResponseType(typeof(Products))]
        public HttpResponseMessage PostProducts(Products products)
        {
            try
            {
                db.Products.Add(products);
                db.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, products);
                message.Headers.Location = new Uri(Request.RequestUri + products.ProductId.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Products))]
        [HttpDelete]
        [Route("api/DeleteProduct/{id}")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            try
            {
                var entity = db.Products.FirstOrDefault(i => i.ProductId == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ProductID " + id + "Not found to delete");
                }
                else
                {
                    db.Products.Remove(entity);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }

            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductsExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }
    }
}