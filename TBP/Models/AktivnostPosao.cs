using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBP.Models
{
    [Table(name:"aktivnost_posao")]
    public class AktivnostPosao
    {
        
        [Column("id_posao")]
        public virtual int PosaoId { get; set; }
        
        [Column("id_aktivnost")]
        public virtual int AktivnostId { get; set; }
    }
}
