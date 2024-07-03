using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OData.Edm;
using NuGet.Packaging.Signing;
using System.Runtime.InteropServices;
using TBP.Models;

namespace TBP.Pages
{
    public class DodajRadniUnos : PageModel
    {
        private readonly ILogger<Home> _logger;
        public readonly PrijavljeniKorisnik _prijavljeniKorisnik;
        private readonly AppDbContext _dbContext;
        [BindProperty]
        public DateTime DatumPocetka { get; set; }
        [BindProperty]
        public DateTime DatumZavrsetka { get; set; }
        [BindProperty]
        public string Opis { get; set; }
        [BindProperty]
        public int OdabraniPosao { get; set; }
        public List<Posao> Poslovi { get; set; }
        public DodajRadniUnos(ILogger<Home> logger, AppDbContext dbContext,PrijavljeniKorisnik prijavljeniKorisnik)
        {
            _logger = logger;
            _dbContext = dbContext;
            _prijavljeniKorisnik = prijavljeniKorisnik;
            Poslovi = _dbContext.Poslovi.ToList<Posao>();
        }
        public void OnGet()
        {
            
        }
        public IActionResult OnPost()
        {
            RadniUnos noviUnos = new RadniUnos();
            noviUnos.Korisnik = _prijavljeniKorisnik.Id;
            noviUnos.DatumPocetka = DatumPocetka;
            noviUnos.DatumZavrsetka = DatumZavrsetka;
            noviUnos.Opis = Opis;
            noviUnos.UkupnoNeto = 0.0;
            noviUnos.Obracunato = false;
            noviUnos.PosaoId = OdabraniPosao;
            _dbContext.RadniUnosi.Add(noviUnos);
            _dbContext.SaveChanges();
            return RedirectToPage("DodajAktivnost", new { id = noviUnos.Id });

        }
    }
}