using Library.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataLayer
{
    public partial class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<BookEntity> Books { get; set; }
        public DbSet<BorrowEntity> Borrows { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configure book entity
            modelBuilder.Entity<BookEntity>(entity =>
            {
                entity.HasKey(e => e.BookID);
                entity.Property(e => e.ISBN).IsRequired();
                entity.Property(e => e.AuthorFirstName).IsRequired();
                entity.Property(e => e.AuthorLastName).IsRequired();
                entity.HasMany(e => e.Borrows)
                      .WithOne(b => b.Book)
                      .HasForeignKey(b => b.BookID);
            });

            //Configure borrow entity
            modelBuilder.Entity<BorrowEntity>(entity =>
            {
                entity.HasKey(e => e.BorrowID);
                entity.Property(e => e.BookID).IsRequired();
                entity.Property(e => e.UserID).IsRequired();
                entity.Property(e => e.BorrowDate).IsRequired();
                entity.HasOne(b => b.Book)
                      .WithMany(b => b.Borrows)
                      .HasForeignKey(b => b.BookID);
                entity.HasOne(b => b.User)
                      .WithMany(u => u.Borrows)
                      .HasForeignKey(b => b.UserID);
            });

            //Configure user entity
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.HasMany(e => e.Borrows)
                      .WithOne(b => b.User)
                      .HasForeignKey(b => b.UserID);
            });
        }
    }
}
