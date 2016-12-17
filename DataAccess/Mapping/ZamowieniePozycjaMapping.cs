using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class ZamowieniePozycjaMapping : EntityTypeConfiguration<ZamowieniePozycja>
    {
        public ZamowieniePozycjaMapping()
        {
            ToTable("ZamowieniaPozycje");

            HasKey(x => x.Id);

            Property(x => x.Ilosc).IsRequired();

            HasRequired(x => x.Towar).WithMany(x => x.PozycjeNaZamowieniach).HasForeignKey(x => x.TowarId);
            HasRequired(x => x.Zamowienie).WithMany(x => x.Pozycje).HasForeignKey(x => x.ZamowienieId);
        }
    }
}
