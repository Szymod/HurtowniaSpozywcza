using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class FakturaSprzedazyMapping : EntityTypeConfiguration<FakturaSprzedazy>
    {
        public FakturaSprzedazyMapping()
        {
            ToTable("FakturySprzedazy");

            HasKey(x => x.Id);

            Property(x => x.NumerFaktury).IsRequired().HasMaxLength(40);
            Property(x => x.DataSprzedazy).IsRequired();
            Property(x => x.DataWystawienia).IsRequired();


            Property(x => x.KwotaBrutto).IsRequired();
            Property(x => x.KwotaNetto).IsRequired();
            Property(x => x.StawkaVat).IsRequired();
            Property(x => x.WartoscVat).IsRequired();

         
            HasRequired(x => x.Klient).WithMany(x => x.Faktury).HasForeignKey(x => x.KlientId);
            HasRequired(x => x.Zamowienie).WithOptional(x => x.Faktura);

            HasMany(x => x.Pozycje).WithRequired(x => x.FakturaSprzedazy).HasForeignKey(x => x.FakturaSprzedazyId);
        }
    }
}
