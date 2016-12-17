using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class UzytkownikMapping : EntityTypeConfiguration<Uzytkownik>
    {
        public UzytkownikMapping()
        {
            ToTable("Uzytkownicy");

            HasKey(x => x.Id);

            Property(x => x.Login).HasMaxLength(126);
            Property(x => x.Haslo).HasMaxLength(126);

            HasMany(x => x.Uprawnienia).WithMany(x => x.Uzytkownicy).Map(x => x.ToTable("UzytkownicyUprawnienia"));
        }
    }
}
