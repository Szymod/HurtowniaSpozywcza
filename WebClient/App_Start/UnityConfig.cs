using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using DataAccess;
using Interfaces;
using Model.DomainModel;

namespace WebClient.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ApplicationDbContext, ApplicationDbContext>(new PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IBaseRepository<Adres>, BaseRepository<Adres>>();
            container.RegisterType<IBaseRepository<Dostawca>, BaseRepository<Dostawca>>();
            container.RegisterType<IBaseRepository<FakturaSprzedazy>, BaseRepository<FakturaSprzedazy>>();
            container.RegisterType<IBaseRepository<FakturaSprzedazyPozycja>, BaseRepository<FakturaSprzedazyPozycja>>();
            container.RegisterType<IBaseRepository<Kategoria>, BaseRepository<Kategoria>>();
            container.RegisterType<IBaseRepository<Klient>, BaseRepository<Klient>>();
            container.RegisterType<IBaseRepository<KodPocztowy>, BaseRepository<KodPocztowy>>();
            container.RegisterType<IBaseRepository<Miasto>, BaseRepository<Miasto>>();
            container.RegisterType<IBaseRepository<Towar>, BaseRepository<Towar>>();
            container.RegisterType<IBaseRepository<TowarHistoria>, BaseRepository<TowarHistoria>>();
            container.RegisterType<IBaseRepository<Ulica>, BaseRepository<Ulica>>();
            container.RegisterType<IBaseRepository<Uzytkownik>, BaseRepository<Uzytkownik>>();
            container.RegisterType<IBaseRepository<UzytkownikUprawnienie>, BaseRepository<UzytkownikUprawnienie>>();
            container.RegisterType<IBaseRepository<Zamowienie>, BaseRepository<Zamowienie>>();
            container.RegisterType<IBaseRepository<ZamowieniePozycja>, BaseRepository<ZamowieniePozycja>>();

        }
    }
}
