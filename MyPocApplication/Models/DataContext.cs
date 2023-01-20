using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyPocApplication.Models
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}