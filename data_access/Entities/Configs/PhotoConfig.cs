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
    internal class PhotoConfig : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Path).HasMaxLength(256);
            builder.HasIndex(x => x.Path).IsUnique();
            builder.ToTable(t => t.HasCheckConstraint("Path_check", "[Path] <> ''"));
        }
    }
}
