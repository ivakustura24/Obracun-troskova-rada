using Microsoft.EntityFrameworkCore;
using TBP.Models;

namespace TBP
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<AktivnostPosao>()
                .HasKey(aktivnostPosao => new { aktivnostPosao.PosaoId, aktivnostPosao.AktivnostId });
            modelBuilder.Entity<RadniUnosAktivnost>()
                .HasKey(UnosAktivnost => new { UnosAktivnost.IdAktivnost, UnosAktivnost.IdUnos });
            modelBuilder.Entity<ObracunPlatnaLista>()
                .HasKey(ObracunLista => new { ObracunLista.IdPlatnaLista, ObracunLista.IdObracun });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=admin;Database=TBP");
        
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Aktivnost> Aktivnosti { get; set; }
        public DbSet<Posao> Poslovi { get; set; }
        public DbSet<AktivnostPosao> AktivnostiPosla { get; set; }
        public DbSet<RadniUnos> RadniUnosi { get; set;}
        public DbSet<RadniUnosAktivnost> AktivnostiRadnogUnosa { get; set; }    
        public DbSet<Obracun> Obracuni { get; set; }
        public DbSet<PlatnaLista> PlatneListe { get; set; }
        public DbSet<ObracunPlatnaLista> ObracuniPlatneListe { get; set; }

    }
}
