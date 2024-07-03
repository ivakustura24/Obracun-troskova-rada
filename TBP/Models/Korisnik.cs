using System.ComponentModel.DataAnnotations.Schema;

namespace TBP.Models
{
    [Table(name: "korisnik")]
    public class Korisnik
    {
        [Column("id_korisnik")]
        public int Id { get; set; }
        [Column("ime")]
        public string Ime { get; set; }
        [Column("prezime")]
        public string Prezime { get; set; }
        [Column("email")]
        public string Email {get; set;}
        [Column("lozinka_hash")]
        public string Lozinka { get; set;}
        [Column("uloga")]
        public int Uloga { get; set;}

    }
}
