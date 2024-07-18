using ExpenseTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public class ExpenseDbContext:DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<Expense>().HasKey(e => e.Id);
            modelBuilder.Entity<User>()
                .HasMany(e => e.Expenses)
                 .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
        }
        public DbSet<User>User { get; set; }
        public DbSet<Expense>Expense { get; set; }



    }
}
