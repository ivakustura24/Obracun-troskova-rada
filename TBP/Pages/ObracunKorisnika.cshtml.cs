using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using TBP.Models;

namespace TBP.Pages
{
    public class ObracunKorisnika : PageModel
    {
        private readonly ILogger<Home> _logger;
        public readonly PrijavljeniKorisnik _prijavljeniKorisnik;
        private readonly AppDbContext _dbContext;
        public List<RadniUnos> RadniUnosiKorisnika { get; set; }
        public int Id { get; set; }
        public ObracunKorisnika(ILogger<Home> logger, AppDbContext dbContext,PrijavljeniKorisnik prijavljeniKorisnik)
        {
            _logger = logger;
            _dbContext = dbContext;
            _prijavljeniKorisnik = prijavljeniKorisnik;
        }
        public void OnGet()
        {
            var id = Request.RouteValues["id"];
            Id = Convert.ToInt32(id);
            RadniUnosiKorisnika = _dbContext.RadniUnosi.Where(unos => unos.Korisnik == Id && unos.Obracunato == false).ToList<RadniUnos>();
        }
        public void OnPost(double koeficijent, int unosId)
        {
            var radniUnos = _dbContext.RadniUnosi.Find(unosId);
            if (radniUnos != null)
            {
                radniUnos.Obracunato = true;
                _dbContext.SaveChanges();
            }
            double ukupnoBruto = radniUnos.UkupnoNeto * koeficijent;
            Obracun obracun = new Obracun();
            obracun.IdUnos = unosId;
            obracun.Moderator = _prijavljeniKorisnik.Id;
            obracun.DatumObracuna = DateTime.Now;
            obracun.UkupnoBruto = ukupnoBruto;
            _dbContext.Obracuni.Add(obracun);
            _dbContext.SaveChanges();
            OnGet();
        }
    }
}