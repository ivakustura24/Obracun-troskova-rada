using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using TBP.Models;

namespace TBP.Pages
{
    public class Registracija : PageModel
    {
        private readonly ILogger<Home> _logger;
        private readonly AppDbContext _dbContext;
        public readonly PrijavljeniKorisnik _prijavljeniKorisnik;
        [BindProperty]
        public Korisnik noviKorisnik { get; set; }
        [BindProperty]
        public bool Moderator { get; set; }
        public Registracija(ILogger<Home> logger, AppDbContext dbContext, PrijavljeniKorisnik prijavljeniKorisnik)
        {
            _logger = logger;
            _dbContext = dbContext;
            _prijavljeniKorisnik = new PrijavljeniKorisnik();
        }
        public void OnGet()
        {
            
        }
        public IActionResult OnPost()
        {
            noviKorisnik.Lozinka = HashirajLozinku( noviKorisnik.Lozinka);
            if (Moderator)
            {
                noviKorisnik.Uloga = 1;
            }
            else
            {
                noviKorisnik.Uloga = 2;
            }
            
            _dbContext.Korisnici.Add(noviKorisnik);
            _dbContext.SaveChanges();
            return RedirectToPage("Index");
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