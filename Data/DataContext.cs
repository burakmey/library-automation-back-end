using library_automation_back_end.Configurations.Models;
using System.Reflection;

namespace library_automation_back_end.Data
{
    public class DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : DbContext(options)
    {
        public DbSet<UserDesire> UserDesires { get; set; }
        public DbSet<DesireSituation> DesireSituations { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BorrowSituation> BorrowSituations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<ReserveSituation> ReserveSituations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBookBorrow> UserBookBorrows { get; set; }
        public DbSet<UserBookReserve> UserBookReserves { get; set; }

        readonly IConfiguration configuration = configuration;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration(configuration));
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
