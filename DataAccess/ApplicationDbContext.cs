using DataAccess.Mapping;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("HurtowniaSpozywcza")
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());

            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ValidateOnSaveEnabled = true;
        }

        public DbSet<Adres> Adresy { get; set; }
        public DbSet<Dostawca> Dostawcy { get; set; }
        public DbSet<FakturaSprzedazy> FakturySprzedazy { get; set; }
        public DbSet<FakturaSprzedazyPozycja> FakturySprzedazyPozycje { get; set; }
        public DbSet<Klient> Klienci { get; set; }
        public DbSet<KodPocztowy> KodyPocztowe { get; set; }
        public DbSet<Miasto> Miasta { get; set; }
        public DbSet<Towar> Towary { get; set; }
        public DbSet<Ulica> Ulice { get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<UzytkownikUprawnienie> UzytkownicyUprawnienia { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<ZamowieniePozycja> ZamowieniaPozycje { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdresMapping());
            modelBuilder.Configurations.Add(new DostawcaMapping());
            modelBuilder.Configurations.Add(new FakturaSprzedazyMapping());
            modelBuilder.Configurations.Add(new FakturaSprzedazyPozycjaMapping());
            modelBuilder.Configurations.Add(new KlientMapping());
            modelBuilder.Configurations.Add(new KodPocztowyMapping());
            modelBuilder.Configurations.Add(new MiastoMapping());
            modelBuilder.Configurations.Add(new TowarMapping());
            modelBuilder.Configurations.Add(new UlicaMapping());
            modelBuilder.Configurations.Add(new UzytkownikMapping());
            modelBuilder.Configurations.Add(new UzytkownikUprawnienieMapping());
            modelBuilder.Configurations.Add(new ZamowienieMapping());
            modelBuilder.Configurations.Add(new ZamowieniePozycjaMapping());
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
