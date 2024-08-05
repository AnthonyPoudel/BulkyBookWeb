using Bulky.DataAccess.Repository.IRepository;
using bulkyBookWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IcategoryRepository Category { get; private set; }
        private readonly ApplicationDBContext _db;
        public UnitOfWork(ApplicationDBContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
