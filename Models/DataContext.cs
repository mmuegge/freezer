using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freezer_MVC.Models
{
    public class DataContext: DbContext
    {
        public DbSet<FoodSupplier> FoodSuppliers { get; set; }
        public DbSet<FoodGroup> FoodGroups { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
