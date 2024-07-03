using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using TBP.Models;

namespace TBP.Pages
{
    public class DodajPosao : PageModel
    {
        private readonly ILogger<Home> _logger;
        public readonly PrijavljeniKorisnik _prijavljeniKorisnik;
        private readonly AppDbContext _dbContext;
        [BindProperty]
        public string NazivPosla { get; set; }  
        public List<Posao> Poslovi { get; set; }
        public List<PosaoAktivnost> AktivnostiPosao = new List<PosaoAktivnost>();
        public DodajPosao(ILogger<Home> logger, AppDbContext dbContext,PrijavljeniKorisnik prijavljeniKorisnik)
        {
            _logger = logger;
            _dbContext = dbContext;
            _prijavljeniKorisnik = prijavljeniKorisnik;
            Poslovi = _dbContext.Poslovi.ToList<Posao>();
            foreach (var posao in Poslovi)
            {
                var aktivnosti = (from p in _dbContext.Poslovi
                                  join pa in _dbContext.AktivnostiPosla on p.Id equals pa.PosaoId
                                  join a in _dbContext.Aktivnosti on pa.AktivnostId equals a.Id
                                  where p.Id == posao.Id
                                  select new
                                  {
                                      aktivnost = a.Naziv

                                  }).ToList();

                List<string> aktivnosti1 = new List<string>();
                foreach (var item in aktivnosti)
                {
                    aktivnosti1.Add(item.aktivnost);
                }
                PosaoAktivnost posaoAktivnost = new PosaoAktivnost();
                posaoAktivnost.Posao = posao.Naziv;
                posaoAktivnost.Aktivnosti = aktivnosti1;
                AktivnostiPosao.Add(posaoAktivnost);
            }
        }
        public void OnGet()
        {
            Poslovi = _dbContext.Poslovi.ToList<Posao>();
        }
        public void OnPost() { 
            var noviPosao = new Posao();
            noviPosao.Naziv = NazivPosla; 
            _dbContext.Poslovi.Add(noviPosao);
            _dbContext.SaveChanges();
            OnGet();
        }
    }
}