using System.ComponentModel.DataAnnotations.Schema;

namespace TBP.Models
{
    [Table(name:"obracun")]
    public class Obracun
    {
        [Column("id_obracun")]
        public int Id { get; set; }
        [Column("id_unos")]
        public int IdUnos { get; set; }
        [Column("moderator")]
        public int Moderator { get; set; }
        [Column("datum_obracuna")]
        public DateTime DatumObracuna { get; set; }
        [Column("ukupno_bruto")]
        public double UkupnoBruto { get; set; }

    }
}
