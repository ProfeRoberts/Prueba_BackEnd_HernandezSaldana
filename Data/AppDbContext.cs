﻿using LoginApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Login> ccloglogin { get; set; }
        public DbSet<User> ccUsers { get; set; }
        public DbSet<RIACatArea> ccRIACat_Areas { get; set; }
    }
}
