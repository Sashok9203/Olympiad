using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities.Configs
{
    internal class OlympiadConfig : IEntityTypeConfiguration<Olympiad_>
    {
        public void Configure(EntityTypeBuilder<Olympiad_> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(t => t.HasCheckConstraint("Year", "Year >= 1896"));
            builder.HasOne(x => x.City).WithMany(x => x.Olympiads).HasForeignKey(x => x.CityId);
            builder.HasOne(x => x.Season).WithMany(x => x.Olympiads).HasForeignKey(x => x.SeasonId);
            builder.Ignore(x=>x.Description);
            builder.Ignore(x => x.SmallDescription);

        }
    }
}
