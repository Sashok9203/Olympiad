using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities.Configs
{
    internal class SportsmanConfig : IEntityTypeConfiguration<Sportsman>
    {
        public void Configure(EntityTypeBuilder<Sportsman> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(56);
            builder.Property(x => x.PhotoPath).HasMaxLength(256);
            builder.ToTable(t => t.HasCheckConstraint("Name_check", "[Name] <> ''"));
            builder.ToTable(t => t.HasCheckConstraint("Birthday_check", "Birthday < getdate()"));
            builder.Ignore(x=>x.FullName);
            builder.Ignore(x => x.BirthdayStr);
            builder.Ignore(x => x.Photo);
            builder.HasOne(x => x.Sport).WithMany(x => x.Sportsmans).HasForeignKey(x=>x.SportId);
            builder.HasOne(x => x.Country).WithMany(x => x.Sportsmens).HasForeignKey(x => x.CountryId);
            builder.HasOne(x => x.Gender).WithMany(x => x.Sportsmens).HasForeignKey(x => x.GenderId);
          
        }
    }
}
