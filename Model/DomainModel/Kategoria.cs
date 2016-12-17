using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class Kategoria
    {
        public Kategoria()
        {
            Towary = new List<Towar>();
        }

        public int Id { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<Towar> Towary { get; set; }
    }
}
