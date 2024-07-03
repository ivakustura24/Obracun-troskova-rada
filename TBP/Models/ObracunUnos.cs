namespace TBP.Models
{
    public class ObracunUnos
    {
        public string ModeratorIme { get; set; }
        public string ModeratorPrezime { get; set; }
        public string PosaoNaziv { get; set; }  
        public double UkupnoNeto { get; set; }
        public string Opis { get; set; }
        public DateTime DatumObracuna { get; set; }
        public double UkupnoBruto { get; set; }
    }
}
