using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // Adam explains this ShoppingCart intro part 4 at 14 min point

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ShoppingItem> ShoppingItem { get; set; }

    }
}
