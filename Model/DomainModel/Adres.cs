using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class Adres
    {
        public Adres()
        {
            Dostawcy = new List<Dostawca>();
            Klienci = new List<Klient>();
        }

        public int Id { get; set; }

        public int KodPocztowyId { get; set; }
        public virtual KodPocztowy KodPocztowy { get; set; }
        public int MiastoId { get; set; }
        public virtual Miasto Miasto { get; set; }
        public int UlicaId { get; set; }
        public virtual Ulica Ulica { get; set; }
        public string NumerDomu { get; set; }
        public string NumerLokalu { get; set; }

        public override string ToString()
        {
            return Ulica.Nazwa + " " + NumerDomu + ", " + KodPocztowy.Kod + " " + Miasto.Nazwa;
        }

        public virtual ICollection<Dostawca> Dostawcy { get; set; }
        public virtual ICollection<Klient> Klienci { get; set; }
    }
}
