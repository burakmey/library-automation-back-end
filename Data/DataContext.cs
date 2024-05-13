namespace library_automation_back_end.Data
{
    public class DataContext : DbContext
    {
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
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Book).WithMany(b => b.BookAuthors).HasForeignKey(ba => ba.BookId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Author).WithMany(a => a.BookAuthors).HasForeignKey(ba => ba.AuthorId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Book).WithMany(b => b.BookCategories).HasForeignKey(bc => bc.BookId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Category).WithMany(c => c.BookCategories).HasForeignKey(bc => bc.CategoryId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserBookBorrow>().HasKey(ubb => new { ubb.UserId, ubb.BookId });
            modelBuilder.Entity<UserBookBorrow>().HasOne(ubb => ubb.User).WithMany(u => u.UserBookBorrows).HasForeignKey(ubb => ubb.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserBookBorrow>().HasOne(ubb => ubb.Book).WithMany(b => b.UserBookBorrows).HasForeignKey(ubb => ubb.BookId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserBookReserve>().HasKey(ubr => new { ubr.UserId, ubr.BookId });
            modelBuilder.Entity<UserBookReserve>().HasOne(ubr => ubr.User).WithMany(u => u.UserBookReserves).HasForeignKey(ubr => ubr.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserBookReserve>().HasOne(ubr => ubr.Book).WithMany(b => b.UserBookReserves).HasForeignKey(ubr => ubr.BookId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
