using System.ComponentModel.DataAnnotations.Schema;

namespace TBP.Models
{
    [Table("platna_lista_obracun")]
    public class ObracunPlatnaLista
    {
        [Column("id_platna_lista")]
        public int IdPlatnaLista { get; set; }
        [Column("id_obracun")]
        public int IdObracun { get; set; }
    }
}
