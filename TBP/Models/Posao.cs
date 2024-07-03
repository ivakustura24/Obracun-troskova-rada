using System.ComponentModel.DataAnnotations.Schema;

namespace TBP.Models
{
    [Table(name:"posao")]
    public class Posao
    {
        [Column("id_posao")]
        public int Id { get; set; }
        [Column("naziv")]
        public string Naziv { get; set; }
    }
}
