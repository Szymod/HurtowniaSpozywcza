using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class UzytkownikUprawnienieMapping : EntityTypeConfiguration<UzytkownikUprawnienie>
    {
        public UzytkownikUprawnienieMapping()
        {
            ToTable("Uprawnienia");

            HasKey(x => x.Id);

            Property(x => x.Uprawnienie).HasMaxLength(255);

            HasMany(x => x.Uzytkownicy).WithMany(x => x.Uprawnienia);
        }
    }
}
