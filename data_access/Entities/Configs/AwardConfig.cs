using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities.Configs
{
    internal class AwardConfig : IEntityTypeConfiguration<Award>
    {
        public void Configure(EntityTypeBuilder<Award> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(56);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.ToTable(t => t.HasCheckConstraint("Name_check", "[Name] <> ''"));
        }
    }
}
