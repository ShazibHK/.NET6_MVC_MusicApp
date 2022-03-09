using IdentityManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUser{ get; set; }
        public DbSet<PlayList> PlayList { get; set; }
        public DbSet<Song> Song { get; set; }
        public DbSet<ErrorLogger> ErrorLogger { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
