using ExpertsBlog.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpertsBlog.Data
{
    // La classe ExpertsBlogDbContext hérite de Microsoft.EntityFrameworkCore.DbContext
    public class ExpertsBlogDbContext : DbContext
    {
        // 4 auto-propriétés de type DbSet<BlogPost>, DbSet<Category>, DbSet<Address> et DbSet<Tag>
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        // Constructeur prenant un DbContextOptions<ExpertsBlogDbContext> en paramètre.
        // Ce constructeur appelle le constructeur de base (DbContext(options))
        public ExpertsBlogDbContext(DbContextOptions<ExpertsBlogDbContext> options) 
            : base(options)
        {
        }
    }
}