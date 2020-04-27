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

        // this is the constuctor for ApplicationDbContext
        // Adam explains at 50 min point in part 4 of ShoppingCart video
        // options is passed here so you can configure additional thinsg about
        // your database context - like if we want to name models didd than way than
        // tables named
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // Adam explains this ShoppingCart intro part 4 at 14 min point

            // Dbsets are representations of our tables
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ShoppingItem> ShoppingItem { get; set; }

    }
}
