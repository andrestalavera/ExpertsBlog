using ExpertsBlog.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpertsBlog.Data
{
    public class ExpertsBlogDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ExpertsBlogDbContext(DbContextOptions<ExpertsBlogDbContext> options) : base(options)
        {
        }
    }
}