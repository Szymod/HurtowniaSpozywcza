using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class TowarMapping : EntityTypeConfiguration<Towar>
    {
        public TowarMapping()
        {
            ToTable("Towary");

            HasKey(x => x.Id);

            Property(x => x.Nazwa).IsRequired().HasMaxLength(255);
            Property(x => x.Cena).IsRequired();
            Property(x => x.Vat).IsRequired();

            HasRequired(x => x.Dostawca).WithMany(x => x.Towary).HasForeignKey(x => x.DostawcaId);

            HasMany(x => x.PozycjeNaZamowieniach).WithRequired(x => x.Towar).HasForeignKey(x => x.TowarId);
        }
    }
}
