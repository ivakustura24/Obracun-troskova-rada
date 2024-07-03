using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBP.Models
{
    [Table(name: "radni_unos_aktivnost")]
    public class RadniUnosAktivnost
    {
        [Column("id_aktivnost")]
        public int IdAktivnost { get; set; }

        [Column("id_unos")]
        public int IdUnos { get; set; }
        [Column("broj_jedinica")]
        public int BrojJedinica { get; set; }
       
        [Column("neto_aktivnost")]
        public double NetoAktivnost { get; set; }
        
    }
}
