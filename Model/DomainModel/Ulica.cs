using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class Ulica
    {
        public Ulica()
        {
            Adresy = new List<Adres>();
        }

        public int Id { get; set; }

        public int MiastoId { get; set; }
        public virtual Miasto Miasto { get; set; }

        public string Nazwa { get; set; }

        public virtual ICollection<Adres> Adresy { get; set; }
    }
}
