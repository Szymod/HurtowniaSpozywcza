using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class MiastoMapping : EntityTypeConfiguration<Miasto>
    {
        public MiastoMapping()
        {
            ToTable("Miasta");

            HasKey(x => x.Id);

            Property(x => x.Nazwa).IsRequired().HasMaxLength(255);

            HasMany(x => x.Ulice).WithRequired(x => x.Miasto).HasForeignKey(x => x.MiastoId);
            HasMany(x => x.Adresy).WithRequired(x => x.Miasto).HasForeignKey(x => x.MiastoId);
        }
    }
}
