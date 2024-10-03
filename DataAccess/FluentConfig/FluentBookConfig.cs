using EF_Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DataAccess.FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> modelBuilder)
        {
            modelBuilder.ToTable("Fluent_Books");
            modelBuilder.HasKey(u => u.BookID);
            modelBuilder.Property(u => u.ISBN).IsRequired();
            modelBuilder.Property(u => u.ISBN).HasMaxLength(20);
            modelBuilder.Ignore(u => u.PriceRange);
            modelBuilder.HasOne(b => b.Fluent_Publisher).WithMany(b => b.Fluent_Books)
                .HasForeignKey(b => b.Publisher_Id);
        }
    }
}
