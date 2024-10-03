using EF_DataAccess.FluentConfig;
using EF_Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Fluent_BookDetail> Fluent_BookDetails { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }
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
            options.UseSqlServer("Server=MLBSRL1-106854;Database=EF;Integrated Security=True;Trust Server Certificate=True;")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name},LogLevel.Information);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(9, 4);
            modelBuilder.Entity<BookAuthorMap>().HasKey(ba => new { ba.Author_Id, ba.Book_Id});


            //Fluent API Code

            modelBuilder.ApplyConfiguration(new FluentBookDetailConfig());
            modelBuilder.ApplyConfiguration(new FluentBookConfig());
            modelBuilder.ApplyConfiguration(new Fluent_PublisherConfig());
            modelBuilder.ApplyConfiguration(new Fluent_AuthorConfig());
            modelBuilder.ApplyConfiguration(new Fluent_BookAuthorMapConfig());

            //Seeding
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
