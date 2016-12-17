using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class Uzytkownik
    {
        public Uzytkownik()
        {
            Uprawnienia = new List<UzytkownikUprawnienie>();
        }

        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }

        public virtual ICollection<UzytkownikUprawnienie> Uprawnienia { get; set; }
    }
}
