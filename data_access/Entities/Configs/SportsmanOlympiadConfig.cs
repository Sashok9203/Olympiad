using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities.Configs
{
    internal class SportsmanOlympiadConfig : IEntityTypeConfiguration<SportsmanOlympiad>
    {
        public void Configure(EntityTypeBuilder<SportsmanOlympiad> builder)
        {
            builder.HasKey(x => new {x.OlympiadId,x.SportsmanId });
            builder.HasOne(x => x.Sportsman).WithMany(x => x.SportsmanOlympiads).HasForeignKey(x => x.SportsmanId);
            builder.HasOne(x => x.Olympiad).WithMany(x => x.SportsmanOlympiads).HasForeignKey(x => x.OlympiadId);
        }
    }
}
