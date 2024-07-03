using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using System.Text.Json;
using TBP.Models;

namespace TBP.Pages
{
    public class Statistika : PageModel
    {
        private readonly ILogger<Home> _logger;
        public readonly PrijavljeniKorisnik _prijavljeniKorisnik;
        private readonly AppDbContext _dbContext;
        public string[] JSONPoslovi = { "Italy", "France", "Spain", "USA", "Argentina" };
        public int[] JSONCijena = { 55, 49, 44, 24, 15 };
        public List<PosaoNeto> PosloviKorisnika { get; set; }
        public Statistika(ILogger<Home> logger, AppDbContext dbContext, PrijavljeniKorisnik prijavljeniKorisnik)
        {
            _logger = logger;
            _dbContext = dbContext;
            _prijavljeniKorisnik = prijavljeniKorisnik;
            PosloviKorisnika = DohvatiPoslove();
        }
        public void OnGet()
        {

        }
        public List<PosaoNeto> DohvatiPoslove()
        {
            var posaoCijena = (from p in _dbContext.Poslovi
                               join ra in _dbContext.RadniUnosi on p.Id equals ra.PosaoId
                               where ra.Korisnik == _prijavljeniKorisnik.Id
                               group ra by p.Naziv into grupaPoslova
                               select new
                               {
                                   posao = grupaPoslova.Key,
                                   suma = grupaPoslova.Sum(ra => ra.UkupnoNeto)
                               }).ToList();
            List<PosaoNeto> posloviNeto = new List<PosaoNeto>();
            foreach (var posao in posaoCijena)
            {
                PosaoNeto posaoNeto = new PosaoNeto();
                posaoNeto.Naziv = posao.posao;
                posaoNeto.Suma = posao.suma;
                posloviNeto.Add(posaoNeto);
            }
            return posloviNeto;
        }
    }
}