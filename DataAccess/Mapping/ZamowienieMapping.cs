using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class ZamowienieMapping : EntityTypeConfiguration<Zamowienie>
    {
        public ZamowienieMapping()
        {
            ToTable("Zamowienia");

            HasKey(x => x.Id);

            Property(x => x.DataZlozeniaZamowienia).IsRequired();

            HasRequired(x => x.Klient).WithMany(x => x.Zamowienia).HasForeignKey(x => x.KlientId);
            HasOptional(x => x.Faktura).WithRequired(x => x.Zamowienie);

            HasMany(x => x.Pozycje).WithRequired(x => x.Zamowienie).HasForeignKey(x => x.ZamowienieId);
        }
    }
}
