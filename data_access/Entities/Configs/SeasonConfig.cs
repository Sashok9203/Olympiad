using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using data_access.Entities;

namespace data_access.Entities.Configs
{
    internal class SeasonConfig : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(56);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.ToTable(t => t.HasCheckConstraint("Name_check", "[Name] <> ''"));
        }
    }
}
