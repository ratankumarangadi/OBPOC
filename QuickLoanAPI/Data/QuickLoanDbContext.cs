using Microsoft.EntityFrameworkCore;
using QuickLoanAPI.Model.DbEntiry;
using QuickLoanAPI.Model.DbEntity;

namespace QuickLoanAPI.Data
{
    public class QuickLoanDbContext : DbContext
    {
        public QuickLoanDbContext(DbContextOptions<QuickLoanDbContext> options) : base(options)
        {

        }
        public DbSet<AccountTypes> AccountTypes { get; set; }
        public DbSet<AccountDetails> AccountDetails { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<Loans> Loans { get; set; }
        public DbSet<Shares> Shares { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Cards> Cards { get; set; }
        public DbSet<BankNames> BankNames { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Permissions> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountTypes>().ToTable("AccountTypes");
            modelBuilder.Entity<AccountDetails>().ToTable("AccAccountDetails");
            modelBuilder.Entity<Accounts>().ToTable("Accounts");
            modelBuilder.Entity<Members>().ToTable("Members");
            modelBuilder.Entity<Loans>().ToTable("Loans");
            modelBuilder.Entity<Shares>().ToTable("Shares");
            modelBuilder.Entity<Transactions>().ToTable("Transactions");
            modelBuilder.Entity<BankNames>().ToTable("Banks");
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Permissions>().ToTable("Permissions");
        }
    }
}
