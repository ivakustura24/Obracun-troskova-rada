using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TBP.Models;

namespace TBP.Pages
{
    public class KreirajAktivnost : PageModel
    {
        private readonly ILogger<Home> _logger;
        public readonly PrijavljeniKorisnik _prijavljeniKorisnik;
        public readonly AppDbContext _dbContext;
        [BindProperty]
        public string Naziv { get; set; }
        [BindProperty]
        public string MjernaJedinica { get; set; }
        [BindProperty]
        public double NetoVrijednost { get; set; }
        [BindProperty]
        public int OdabraniPosao { get; set; }
        public List<Aktivnost> aktivnosti { get; set; } 
        public List<Posao> Poslovi { get; set; }
        public KreirajAktivnost(ILogger<Home> logger, AppDbContext dbContext, PrijavljeniKorisnik prijavljeniKorisnik)
        {
            _logger = logger;
            _dbContext = dbContext; 
            _prijavljeniKorisnik = prijavljeniKorisnik;
            aktivnosti = _dbContext.Aktivnosti.ToList<Aktivnost>();
            Poslovi = _dbContext.Poslovi.ToList<Posao>();
            
        }
        public void OnGet()
        {
            aktivnosti = _dbContext.Aktivnosti.ToList<Aktivnost>();
            Poslovi = _dbContext.Poslovi.ToList<Posao>();
        }
        public void OnPost() 
        {
            
            Aktivnost novaAktivnost = new Aktivnost();
            novaAktivnost.Naziv = Naziv;
            novaAktivnost.Jedinica = MjernaJedinica;
            novaAktivnost.Neto = NetoVrijednost;
            _dbContext.Aktivnosti.Add(novaAktivnost);
            _dbContext.SaveChanges();
            AktivnostPosao novaAktivnostPosao = new AktivnostPosao();
            novaAktivnostPosao.PosaoId = OdabraniPosao;
            novaAktivnostPosao.AktivnostId = novaAktivnost.Id;
            _dbContext.AktivnostiPosla.Add(novaAktivnostPosao);
            _dbContext.SaveChanges();
            OnGet();
            
        }
    }
}