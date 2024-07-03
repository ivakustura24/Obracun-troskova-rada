using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using TBP.Models;

namespace TBP.Pages
{
    public class PlatnaListaModel : PageModel
    {
        private readonly ILogger<Home> _logger;
        public readonly PrijavljeniKorisnik _prijavljeniKorisnik;
        private readonly AppDbContext _dbContext;
        public PlatnaLista PlatnaListaKorisnika { get; set; }
        public PlatnaListaObracun PlatnaListaObracuni { get; set; }
        public PlatnaListaModel(ILogger<Home> logger, AppDbContext dbContext,PrijavljeniKorisnik prijavljeniKorisnik)
        {
            _logger = logger;
            _dbContext = dbContext;
            _prijavljeniKorisnik = prijavljeniKorisnik;
            PlatnaListaKorisnika = _dbContext.PlatneListe.Where(platnaLista => platnaLista.IdKorisnik == _prijavljeniKorisnik.Id).OrderBy(platnalista => platnalista.Id).LastOrDefault();
            PlatnaListaObracuni = new PlatnaListaObracun();
            PlatnaListaObracuni.PlatnaListaKorisnika = PlatnaListaKorisnika;
            PlatnaListaObracuni.Obracuni = DohvatiObracune();
        }

        private List<ObracunUnos> DohvatiObracune()
        {
            List<ObracunUnos> obracuniPlatneListe = new List<ObracunUnos>();
            var obracuni = (from p in _dbContext.PlatneListe
                            join po in _dbContext.ObracuniPlatneListe on p.Id equals po.IdPlatnaLista
                            join o in _dbContext.Obracuni on po.IdObracun equals o.Id
                            join ra in _dbContext.RadniUnosi on o.IdUnos equals ra.Id
                            join pos in _dbContext.Poslovi on ra.PosaoId equals pos.Id
                            join k in _dbContext.Korisnici on o.Moderator equals k.Id
                            where p.Id == PlatnaListaKorisnika.Id 
                            select new
                            {
                                posao_naziv = pos.Naziv,
                                moderator_ime = k.Ime,
                                moderator_prezime = k.Prezime,
                                opis = ra.Opis,
                                datum_obracuna = o.DatumObracuna,
                                ukupno_neto = ra.UkupnoNeto,
                                ukupno_bruto = o.UkupnoBruto


                            }).ToList();
            foreach (var o in obracuni)
            {
                ObracunUnos noviObracun = new ObracunUnos();
                noviObracun.PosaoNaziv = o.posao_naziv;
                noviObracun.ModeratorIme = o.moderator_ime;
                noviObracun.ModeratorPrezime = o.moderator_prezime;
                noviObracun.DatumObracuna = o.datum_obracuna;
                noviObracun.UkupnoBruto = o.ukupno_neto;
                noviObracun.UkupnoBruto = Math.Round(o.ukupno_bruto,2);
                obracuniPlatneListe.Add(noviObracun);
            }
            return obracuniPlatneListe;
        }

        public void OnGet()
        {
            
        }
    }
}