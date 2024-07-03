using Microsoft.OData.Edm;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBP.Models
{
    [Table(name:"radni_unos")]
    public class RadniUnos
    {
        [Column("id_unos")]
        public int Id { get; set; }
        [Column("korisnik")]
        public virtual int Korisnik { get; set; }
        [Column("datum_pocetka")]
        public DateTime DatumPocetka { get; set; }
        [Column("datum_zavrsetka")]
        public DateTime DatumZavrsetka { get; set; }
        [Column("opis")] 
        public string? Opis { get; set; }
        [Column("ukupno_neto")]
        public double UkupnoNeto { get; set; }
        [Column("obracunato")]
        public bool Obracunato { get; set; }
        [Column("id_posao")]
        public int PosaoId { get; set; }
    }
}
