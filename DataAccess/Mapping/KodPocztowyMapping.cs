using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class KodPocztowyMapping : EntityTypeConfiguration<KodPocztowy>
    {
        public KodPocztowyMapping()
        {
            ToTable("KodyPocztowe");

            HasKey(x => x.Id);

            Property(x => x.Kod).IsRequired().HasMaxLength(255);

            HasMany(x => x.Miasta).WithRequired(x => x.KodPocztowy).HasForeignKey(x => x.KodPocztowyId).WillCascadeOnDelete(false);
            HasMany(x => x.Adresy).WithRequired(x => x.KodPocztowy).HasForeignKey(x => x.KodPocztowyId);
        }
    }
}
