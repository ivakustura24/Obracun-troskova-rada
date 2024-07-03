using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using TBP.Models;

namespace TBP.Pages
{
    public class PrikazKorisnika : PageModel
    {
        private readonly ILogger<Home> _logger;
        private readonly AppDbContext _dbContext;
        public readonly PrijavljeniKorisnik _prijavljeniKorisnik;
        public List<Korisnik> korisnici { get; set; }
        public PrikazKorisnika(ILogger<Home> logger, AppDbContext dbContext, PrijavljeniKorisnik prijavljeniKorisnik)
        {
            _logger = logger;
            _dbContext = dbContext;
            _prijavljeniKorisnik = prijavljeniKorisnik;
        }
     
        public void OnGet()
        {
            korisnici = _dbContext.Korisnici.ToList<Korisnik>();
            korisnici = korisnici.Where(korisnik => korisnik.Uloga == 2).ToList<Korisnik>();
        }
    }
}