using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class KlientMapping : EntityTypeConfiguration<Klient>
    {
        public KlientMapping()
        {
            ToTable("Klienci");

            HasKey(x => x.Id);

            Property(x => x.Nazwa).IsRequired().HasMaxLength(255);

            HasRequired(x => x.Adres).WithMany(x => x.Klienci).HasForeignKey(x => x.AdresId).WillCascadeOnDelete(false);

            HasMany(x => x.Faktury).WithRequired(x => x.Klient).HasForeignKey(x => x.KlientId);
            HasMany(x => x.Zamowienia).WithRequired(x => x.Klient).HasForeignKey(x => x.KlientId);
        }
    }
}
