using Interfaces;
using Microsoft.Practices.Unity;
using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private ApplicationDbContext DbContext { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            DbContext = context;
        }

        [Dependency]
        public IBaseRepository<Adres> Adresy { get; set; }

        [Dependency]
        public IBaseRepository<Dostawca> Dostawcy { get; set; }

        [Dependency]
        public IBaseRepository<FakturaSprzedazy> FakturySprzedazy { get; set; }

        [Dependency]
        public IBaseRepository<FakturaSprzedazyPozycja> FakturySprzedazyPozycje { get; set; }

        [Dependency]
        public IBaseRepository<Klient> Klienci { get; set; }

        [Dependency]
        public IBaseRepository<KodPocztowy> KodyPocztowe { get; set; }

        [Dependency]
        public IBaseRepository<Miasto> Miasta { get; set; }

        [Dependency]
        public IBaseRepository<Towar> Towary { get; set; }

        [Dependency]
        public IBaseRepository<Ulica> Ulice { get; set; }

        [Dependency]
        public IBaseRepository<Uzytkownik> Uzytkownicy { get; set; }

        [Dependency]
        public IBaseRepository<UzytkownikUprawnienie> UzytkownicyUprawnienia { get; set; }

        [Dependency]
        public IBaseRepository<Zamowienie> Zamowienia { get; set; }

        [Dependency]
        public IBaseRepository<ZamowieniePozycja> ZamowieniaPozycje { get; set; }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public bool HasChanges()
        {
            return DbContext.ChangeTracker.HasChanges();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }

        }
        #endregion
    }
}