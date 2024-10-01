using EF_Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DataAccess.Data
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Fluent_BookDetail> Fluent_BookDetails { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        //public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        //public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }
        //public DbSet<Fluent_BookAuthorMap> Fluent_BookAuthorMaps { get; set; }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<SubCategory> SubCategorys { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<BookAuthorMap> BookAuthorMaps { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=MLBSRL1-106854;Database=SSIS;Integrated Security=True;Trust Server Certificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(9, 4);
            modelBuilder.Entity<BookAuthorMap>().HasKey(ba => new { ba.Author_Id, ba.Book_Id});


            //Fluent API Code
            modelBuilder.Entity<Fluent_BookDetail>().ToTable("BookDetail_Fluent");
            modelBuilder.Entity<Fluent_BookDetail>().Property(u => u.NumberOfChapters).HasColumnName("NumberOfChapters");
            modelBuilder.Entity<Fluent_BookDetail>().Property(u => u.NumberOfChapters).IsRequired();
            modelBuilder.Entity<Fluent_BookDetail>().HasKey(u => u.BookDetail_Id);
            modelBuilder.Entity<Fluent_BookDetail>().HasOne(b => b.Fluent_Book).WithOne(b => b.Fluent_BookDetail)
                .HasForeignKey<Fluent_BookDetail>("BookID");


            modelBuilder.Entity<Fluent_Book>().ToTable("Book_Fluent");
            modelBuilder.Entity<Fluent_Book>().HasKey(u => u.BookID);
            modelBuilder.Entity<Fluent_Book>().Property(u => u.ISBN).IsRequired();
            modelBuilder.Entity<Fluent_Book>().Property(u => u.ISBN).HasMaxLength(20);
            modelBuilder.Entity<Fluent_Book>().Ignore(u => u.PriceRange);

            modelBuilder.Entity<Book>().HasData(
                    new Book { BookID = 1, Title="Spider Man", ISBN="12345678", Price=11.99m, Publisher_Id=1},
                    new Book { BookID = 2, Title = "Super Man", ISBN = "12345678", Price = 21.99m, Publisher_Id = 1 }
                );

            var BookList = new Book[] {
                new Book { BookID = 3, Title="Iron Man", ISBN="12345678", Price=41.99m, Publisher_Id=2},
                new Book { BookID = 4, Title = "ADO Man", ISBN = "12345678", Price = 61.99m, Publisher_Id=3 }
            };

            modelBuilder.Entity<Book>().HasData(BookList);

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Publisher_Id = 1, Name="Swan", Location="Hawaii"},
                new Publisher { Publisher_Id = 2, Name = "Longman", Location = "NY" },
                new Publisher { Publisher_Id = 3, Name = "Apress", Location = "Chicago" }
            );
        }
    }

}
