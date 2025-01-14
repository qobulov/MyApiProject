using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Model;

namespace MyApiProject.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comment { get; set; }
    }
}