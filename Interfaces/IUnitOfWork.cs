using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Adres> Adresy { get; }
        IBaseRepository<Dostawca> Dostawcy { get; }
        IBaseRepository<FakturaSprzedazy> FakturySprzedazy { get; }
        IBaseRepository<FakturaSprzedazyPozycja> FakturySprzedazyPozycje { get; }
        IBaseRepository<Klient> Klienci { get; }
        IBaseRepository<KodPocztowy> KodyPocztowe { get; }
        IBaseRepository<Miasto> Miasta { get; }
        IBaseRepository<Towar> Towary { get; }
        IBaseRepository<Ulica> Ulice { get; }
        IBaseRepository<Uzytkownik> Uzytkownicy { get; }
        IBaseRepository<UzytkownikUprawnienie> UzytkownicyUprawnienia { get; }
        IBaseRepository<Zamowienie> Zamowienia { get; }
        IBaseRepository<ZamowieniePozycja> ZamowieniaPozycje { get; }
        void Dispose();
        void Commit();
        Task CommitAsync();
        bool HasChanges();
    }
}
