using BookShop.Models;
using Microsoft.EntityFrameworkCore;


namespace BookShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<EducationYear> EducationYears { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
