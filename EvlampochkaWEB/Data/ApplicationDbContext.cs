using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EvlampochkaWEB.Models;

namespace EvlampochkaWEB.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EvlampochkaWEB.Models.Collection> Collection { get; set; }
        public DbSet<EvlampochkaWEB.Models.Item> Item { get; set; }
        public DbSet<EvlampochkaWEB.Models.Tag> Tag { get; set; }
        public DbSet<EvlampochkaWEB.Models.Comment> Comment { get; set; }
        public DbSet<EvlampochkaWEB.Models.Like> Like { get; set; }
    }
}
