using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class KategoriaMapping : EntityTypeConfiguration<Kategoria>
    {
        public KategoriaMapping()
        {
            ToTable("Kategorie");

            HasKey(x => x.Id);

            Property(x => x.Nazwa).IsRequired().HasMaxLength(255);
            HasMany(x => x.Towary).WithRequired(x => x.Kategoria).HasForeignKey(x => x.KategoriaId);
        }
    }
}
