﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAngularApp.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<CustomersReviews> CustomerReviews { get; set; }
    }
}
