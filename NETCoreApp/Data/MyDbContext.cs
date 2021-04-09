using Microsoft.EntityFrameworkCore;
using NETCoreApp.Models;

namespace NETCoreApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .Property(b => b.CreatedDate).HasDefaultValueSql("GETDATE()");

            builder.Entity<Book>()
                .Property(b => b.ModifiedDate).HasDefaultValueSql("GETDATE()");

            builder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired();
        }
    }
}