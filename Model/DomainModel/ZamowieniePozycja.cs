using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class ZamowieniePozycja
    {
        public int Id { get; set; }
        public int ZamowienieId { get; set; }
        public virtual Zamowienie Zamowienie { get; set; }
        public int TowarId { get; set; }
        public virtual Towar Towar { get; set; }
        public int Ilosc { get; set; }
    }
}
