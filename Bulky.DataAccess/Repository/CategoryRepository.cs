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
    public class CategoryRepository : Repository<Category>, IcategoryRepository
    {
        private readonly ApplicationDBContext _db;
        public CategoryRepository(ApplicationDBContext db): base(db)
        {
            _db = db;
        }
       

        public void update(Category obj)
        {
            _db.Update(obj);
        }
    }
}
