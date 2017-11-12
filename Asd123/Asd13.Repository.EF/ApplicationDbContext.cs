using Asd123.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Asd123.Repository.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ImageInfo> ImageInfos { get; set; }
    }
}
