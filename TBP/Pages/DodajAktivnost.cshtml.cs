using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using TBP.Models;

namespace TBP.Pages
{
    public class DodajAktivnost : PageModel
    {
        private readonly ILogger<Home> _logger;
        public readonly PrijavljeniKorisnik _prijavljeniKorisnik;
        private readonly AppDbContext _dbContext;
        private int IdRadniUnos { get; set; }
        public RadniUnos OdabraniUnos { get; set; }
        public List<Aktivnost> Aktivnosti { get; set; }
        [BindProperty]
        public int OdabranaAktivnost { get; set; }
        [BindProperty]
        public int BrojJedinica { get; set; }
        public List<string> AktivnostiUnosa { get; set; }
        public DodajAktivnost(ILogger<Home> logger, AppDbContext dbContext,PrijavljeniKorisnik prijavljeniKorisnik)
        {
            _logger = logger;
            _dbContext = dbContext;
            _prijavljeniKorisnik = prijavljeniKorisnik;
           
        }
        public void OnGet(int id)
        {
            IdRadniUnos = id;
            OdabraniUnos = _dbContext.RadniUnosi.Where(radniUnos => radniUnos.Id == IdRadniUnos).First();
            Aktivnosti = DohvatiAktivnosti(OdabraniUnos);
            var aktivnosti = (from a in _dbContext.Aktivnosti
                              join ra in _dbContext.AktivnostiRadnogUnosa on a.Id equals ra.IdAktivnost
                              join r in _dbContext.RadniUnosi on ra.IdUnos equals r.Id
                              where r.Id == id
                              select new
                              {
                                  aktivnost = a.Naziv

                              }).ToList();
            List<string> aktivnosti1 = new List<string>();
            if (aktivnosti != null)
            {
                foreach(var aktivnost in aktivnosti)
                {
                    aktivnosti1.Add(aktivnost.aktivnost);
                }
                AktivnostiUnosa = aktivnosti1;
            }
            
        }

        private List<Aktivnost> DohvatiAktivnosti(RadniUnos odabraniUnos)
        {
            List<Aktivnost> values = new List<Aktivnost>();
            List<AktivnostPosao> aktivnostiPosla = _dbContext.AktivnostiPosla.Where(aktivnostiPosla => aktivnostiPosla.PosaoId == odabraniUnos.PosaoId).ToList<AktivnostPosao>();
            List<Aktivnost> SveAktivnosti = _dbContext.Aktivnosti.ToList<Aktivnost>();
            foreach (var aktivnost in SveAktivnosti)
            {
                foreach (var aktivnostPosao in aktivnostiPosla)
                {
                    if (aktivnost.Id == aktivnostPosao.AktivnostId)
                    {
                        values.Add(aktivnost);
                    }
                }
            }
            return values;
        }

        public void OnPost(int id) {
            double NetoVrijednostAktivnosti = _dbContext.Aktivnosti.Where(aktivnost => aktivnost.Id == OdabranaAktivnost).First().Neto;
            double NetoUkupno = NetoVrijednostAktivnosti * BrojJedinica;
            RadniUnosAktivnost novaAktivnostRada= new RadniUnosAktivnost();
            novaAktivnostRada.IdAktivnost = OdabranaAktivnost;
            novaAktivnostRada.IdUnos = id;
            novaAktivnostRada.BrojJedinica = BrojJedinica;
            novaAktivnostRada.NetoAktivnost = NetoUkupno;
            _dbContext.AktivnostiRadnogUnosa.Add(novaAktivnostRada);
            _dbContext.SaveChanges();
            OnGet(id);
        }
    }
}