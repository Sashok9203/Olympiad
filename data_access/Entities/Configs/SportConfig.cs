using data_access.Entityes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities.Configs
{
    internal class SportConfig : IEntityTypeConfiguration<Sport>
    {
        public void Configure(EntityTypeBuilder<Sport> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(56);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.ToTable(t => t.HasCheckConstraint("Name_check", "[Name] <> ''"));
            builder.HasOne(x => x.Season).WithMany(x => x.Sports).HasForeignKey(x => x.SeasonId);
        }
    }
}
