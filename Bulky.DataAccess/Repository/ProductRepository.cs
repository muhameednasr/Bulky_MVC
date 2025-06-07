using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            var objFtomDb = _db.Products.FirstOrDefault(u=> u.Id == obj.Id);
            if (objFtomDb != null)
            {
                objFtomDb.Title = obj.Title;
                objFtomDb.ISBN = obj.ISBN;
                objFtomDb.Author = obj.Author;
                objFtomDb.ListPrice = obj.ListPrice;
                objFtomDb.Price = obj.Price;
                objFtomDb.Price50 = obj.Price50;
                objFtomDb.Price100 = obj.Price100;
                objFtomDb.CategoryId = obj.CategoryId;
                if (obj.ImageURL != null)
                {
                    objFtomDb.ImageURL = obj.ImageURL;
                }
            }
                
        }
    }
}
