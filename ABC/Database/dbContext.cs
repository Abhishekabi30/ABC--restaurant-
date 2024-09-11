using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using ABC.Model;
using ABC.Model;

namespace ABC.Database
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {


        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Resturant> Resturants { get; set; }
        public DbSet<Offer> Offers { get; set; }










    }
}

