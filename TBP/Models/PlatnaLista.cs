using System.ComponentModel.DataAnnotations.Schema;

namespace TBP.Models
{
    [Table(name:"platna_lista")]
    public class PlatnaLista
    {
        [Column("id_platna_lista")]
        public int Id { get; set; }
        [Column("datum_pocetka")]
        public DateOnly DatumPocetka { get; set; }
        [Column("datum_zavrsetka")]
        public DateOnly DatumZavrsetka { get; set; }
        [Column("ukupno")]
        public double Ukupno { get; set; }
        [Column("id_korisnik")]
        public int IdKorisnik { get; set; }
    }
}
