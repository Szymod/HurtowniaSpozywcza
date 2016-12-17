using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccess.Mapping
{
    public class UlicaMapping : EntityTypeConfiguration<Ulica>
    {
        public UlicaMapping()
        {
            ToTable("Ulice");

            HasKey(x => x.Id);

            Property(x => x.Nazwa).IsRequired().HasMaxLength(255);

            HasRequired(x => x.Miasto).WithMany(x => x.Ulice).HasForeignKey(x => x.MiastoId).WillCascadeOnDelete(false);
            HasMany(x => x.Adresy).WithRequired(x => x.Ulica).HasForeignKey(x => x.UlicaId);
        }
    }
}
