using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class FakturaSprzedazyPozycjaMapping : EntityTypeConfiguration<FakturaSprzedazyPozycja>
    {
        public FakturaSprzedazyPozycjaMapping()
        {
            ToTable("FakturySprzedazyPozycje");

            HasKey(x => x.Id);

            Property(x => x.CenaJednostkowa).IsRequired();
            Property(x => x.Ilosc).IsRequired();
            Property(x => x.KwotaBrutto).IsRequired();
            Property(x => x.KwotaNetto).IsRequired();
            Property(x => x.StawkaVat).IsRequired();
            Property(x => x.WartoscVat).IsRequired();

            HasRequired(x => x.FakturaSprzedazy).WithMany(x => x.Pozycje).HasForeignKey(x => x.FakturaSprzedazyId);
        }
    }
}
