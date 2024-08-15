using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using bulkyBookWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDBContext _db;
        public ProductRepository(ApplicationDBContext db): base(db)
        {
            _db = db;
        }
       

        public void update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
