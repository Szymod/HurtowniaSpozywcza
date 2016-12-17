using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class TowarHistoriaMapping : EntityTypeConfiguration<TowarHistoria>
    {
        public TowarHistoriaMapping()
        {
            ToTable("TowaryHistoria");

            HasKey(x => x.Id);

            Property(x => x.Cena).IsRequired();
            Property(x => x.Vat).IsRequired();

            Property(x => x.DataPoczatkuObowiazywania).IsRequired();
            Property(x => x.DataKoncaObowiazywania).IsRequired();

            HasRequired(x => x.Towar).WithMany(x => x.CenaHistoria).HasForeignKey(x => x.TowarId);
        }
    }
}
