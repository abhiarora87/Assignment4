using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Assignment4.Models.EF_Models;
using static Assignment4.Models.Sales_Model;

namespace Assignment4.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Automobile> Automobiles { get; set; }
        public DbSet<Sales> Sales { get; set; }

        public DbSet<Vehicle> Vehicle { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Salesperson> Salesperson { get; set; }
        //public DbSet<Sa>

    }
}
