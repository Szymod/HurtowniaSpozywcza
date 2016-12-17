using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class UzytkownikUprawnienie
    {
        public UzytkownikUprawnienie()
        {
            Uzytkownicy = new List<Uzytkownik>();
        }

        public int Id { get; set; }
        public string Uprawnienie { get; set; }

        public virtual ICollection<Uzytkownik> Uzytkownicy { get; set; } 
    }
}
