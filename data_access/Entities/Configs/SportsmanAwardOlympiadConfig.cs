using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities.Configs
{
    internal class SportsmanAwardOlympiadConfig : IEntityTypeConfiguration<SportsmanAwardOlympiad>
    {
        public void Configure(EntityTypeBuilder<SportsmanAwardOlympiad> builder)
        {
            builder.HasKey(x => new {x.OlympiadId,x.SportsmanId });
            builder.HasOne(x => x.Sportsman).WithMany(x => x.AwardOlympiads)
                    .HasForeignKey(x => x.SportsmanId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Olympiad).WithMany(x => x.SportsmanAward)
                    .HasForeignKey(x => x.OlympiadId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Award).WithMany(x => x.SportsmanOlympiads)
                   .HasForeignKey(x => x.AwardId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
