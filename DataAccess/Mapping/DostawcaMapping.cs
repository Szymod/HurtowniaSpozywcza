using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class DostawcaMapping : EntityTypeConfiguration<Dostawca>
    {
        public DostawcaMapping()
        {
            ToTable("Dostawcy");

            HasKey(x => x.Id);

            Property(x => x.Nazwa).IsRequired().HasMaxLength(255);
            HasRequired(x => x.Adres).WithMany(x => x.Dostawcy).HasForeignKey(x => x.AdresId).WillCascadeOnDelete(false);
        }
    }
}
