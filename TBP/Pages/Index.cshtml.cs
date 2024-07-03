using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TBP.Models;
using System.Runtime.CompilerServices;
using System.Text;
using System.Security.Cryptography;

namespace TBP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDbContext _dbContext;
        public PrijavljeniKorisnik _prijavljeniKorisnik;

        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string lozinka { get; set; }

        public IndexModel(ILogger<IndexModel> logger, AppDbContext dbContext, PrijavljeniKorisnik prijavljeniKorisnik)
        {
            _logger = logger;
            _dbContext = dbContext;
            _prijavljeniKorisnik = prijavljeniKorisnik;
        }
        public Korisnik Korisnik { get; set; }
        public void OnGet()
        {
            _prijavljeniKorisnik = new PrijavljeniKorisnik();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var isValidUser = await ProvjeriPrijavuAsync(email, lozinka);
                
                if (isValidUser)
                {
                    if(_prijavljeniKorisnik.Uloga == 1)
                    {
                        return RedirectToPage("PrikazKorisnika");
                    }
                    else
                    {
                        return RedirectToPage("DodajRadniUnos");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Neuspješna prijava. Provjerite e-mail i lozinku.");
                }
            }
            return Page();
        }

        private Task<bool> ProvjeriPrijavuAsync(string email, string lozinka)
        {
            List<Korisnik> korisnici = _dbContext.Korisnici.ToList<Korisnik>();
            string loziznkaSha = HashirajLozinku(lozinka);
            Korisnik = korisnici.Where(korisnik => korisnik.Email == email && korisnik.Lozinka == loziznkaSha).FirstOrDefault();
            if(Korisnik != null)
            {
                _prijavljeniKorisnik.Id= Korisnik.Id;
                _prijavljeniKorisnik.Ime = Korisnik.Ime;
                _prijavljeniKorisnik.Uloga = Korisnik.Uloga;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        private string HashirajLozinku(string lozinka)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] lozinkaSha = Encoding.UTF8.GetBytes(lozinka);
                byte[] hashBytes = sha256.ComputeHash(lozinkaSha);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }
}