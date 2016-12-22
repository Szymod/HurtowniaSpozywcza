using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class Towar
    {
        public Towar()
        {
            PozycjeNaZamowieniach = new List<ZamowieniePozycja>();
        }

        public int Id { get; set; }
        public string Nazwa { get; set; }

        public int DostawcaId { get; set; }
        public Dostawca Dostawca { get; set; }

        public decimal Cena { get; set; }
        public decimal Vat { get; set; }

        public int StanMagazynowy { get; set; }
        public bool Wycofany { get; set; }

        public virtual ICollection<ZamowieniePozycja> PozycjeNaZamowieniach { get; set; }
    }
}
