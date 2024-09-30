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
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=MLBSRL1-106854;Database=SSIS;Integrated Security=True;Trust Server Certificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(9, 4);
            modelBuilder.Entity<Book>().HasData(
                    new Book { BookID = 1, Title="Spider Man", ISBN="12345678", Price=11.99m},
                    new Book { BookID = 2, Title = "Super Man", ISBN = "12345678", Price = 21.99m }
                );

            var BookList = new Book[] {
                new Book { BookID = 3, Title="Iron Man", ISBN="12345678", Price=41.99m},
                new Book { BookID = 4, Title = "ADO Man", ISBN = "12345678", Price = 61.99m }
            };

            modelBuilder.Entity<Book>().HasData(BookList);
        }
    }

}
