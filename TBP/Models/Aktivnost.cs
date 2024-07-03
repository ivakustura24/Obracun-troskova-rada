using System.ComponentModel.DataAnnotations.Schema;

namespace TBP.Models
{
    [Table(name: "aktivnost")]
    public class Aktivnost
    {
        [Column("id_aktivnost")]
        public int Id { get; set; }
        [Column("naziv")]
        public string? Naziv { get; set; }
        [Column("mjerna_jedinica")]
        public string? Jedinica { get; set; }
        [Column("neto_vrijednost")]
        public double Neto { get; set; }
    }
}
