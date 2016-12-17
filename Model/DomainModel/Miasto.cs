using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class Miasto
    {
        public Miasto()
        {
            Ulice = new List<Ulica>();
            Adresy = new List<Adres>();
        }

        public int Id { get; set; }

        public int KodPocztowyId { get; set; }
        public KodPocztowy KodPocztowy { get; set; }

        public string Nazwa { get; set; }

        public virtual ICollection<Adres> Adresy { get; set; }
        public virtual ICollection<Ulica> Ulice { get; set; }
    }
}
