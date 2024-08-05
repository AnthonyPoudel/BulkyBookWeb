using BulkyBookRazor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Generic;

namespace BulkyBookRazor.Data
{
    public class ApplicationDBContext: DbContext
    {
     
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
                
        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
            new Category { Id = 2, Name = "Fictional", DisplayOrder = 2 },
           new Category { Id = 3, Name = "Comics", DisplayOrder = 3 });
           
        }
    }
}
