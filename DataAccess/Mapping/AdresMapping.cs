using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class AdresMapping : EntityTypeConfiguration<Adres>
    {
        public AdresMapping()
        {
            ToTable("Adresy");

            HasKey(x => x.Id);

            Property(x => x.NumerDomu).IsRequired().HasMaxLength(25);
            Property(x => x.NumerLokalu).HasMaxLength(25);

            HasRequired(x => x.KodPocztowy).WithMany(x => x.Adresy).HasForeignKey(x => x.KodPocztowyId);
            HasRequired(x => x.Miasto).WithMany(x => x.Adresy).HasForeignKey(x => x.MiastoId);
            HasRequired(x => x.Ulica).WithMany(x => x.Adresy).HasForeignKey(x => x.UlicaId);

            HasMany(x => x.Dostawcy).WithRequired(x => x.Adres).HasForeignKey(x => x.AdresId).WillCascadeOnDelete(false);
            HasMany(x => x.Klienci).WithRequired(x => x.Adres).HasForeignKey(x => x.AdresId).WillCascadeOnDelete(false);
        }
    }
}
