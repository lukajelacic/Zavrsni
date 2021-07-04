using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eMusicStudio.Web.Models
{
    public partial class _150192V1Context : DbContext
    {
        public _150192V1Context()
        {
        }

        public _150192V1Context(DbContextOptions<_150192V1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Grad> Grad { get; set; }
        public virtual DbSet<Klijenti> Klijenti { get; set; }
        public virtual DbSet<Korisnici> Korisnici { get; set; }
        public virtual DbSet<MuzickaOprema> MuzickaOprema { get; set; }
        public virtual DbSet<Ocjene> Ocjene { get; set; }
        public virtual DbSet<RezervacijaStavke> RezervacijaStavke { get; set; }
        public virtual DbSet<Rezervacije> Rezervacije { get; set; }
        public virtual DbSet<RezervacijeGluveSobe> RezervacijeGluveSobe { get; set; }
        public virtual DbSet<Termini> Termini { get; set; }
        public virtual DbSet<Uloge> Uloge { get; set; }
        public virtual DbSet<Vrsta> Vrsta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=150192V1;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            

            modelBuilder.Entity<Grad>(entity =>
            {
                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Klijenti>(entity =>
            {
                entity.HasKey(e => e.KlijentId)
                    .HasName("PK__Klijenti__5F05D8AEEDF14F69");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.KorisnickoIme)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LozinkaHash)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LozinkaSalt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telefon).HasMaxLength(50);

                entity.HasOne(d => d.Grad)
                    .WithMany(p => p.Klijenti)
                    .HasForeignKey(d => d.GradId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradId");
            });

            modelBuilder.Entity<Korisnici>(entity =>
            {
                entity.HasKey(e => e.KorisnikId)
                    .HasName("PK__Korisnic__80B06D41F5FF910E");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.KorisnickoIme)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LozinkaHash)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LozinkaSalt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telefon).HasMaxLength(50);
                entity.HasOne(d => d.Uloga)
                      .WithMany(x=>x.Korisnici)
                      .HasForeignKey(d => d.UlogaId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UlogaId_Korisnici");
                entity.HasMany(x => x.Termini)
                      .WithOne(x => x.Korisnik)
                      .HasForeignKey(x => x.KorisnikId)
                      .OnDelete(DeleteBehavior.Cascade);
                      
            });

         

            modelBuilder.Entity<MuzickaOprema>(entity =>
            {
                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(100);

              

                entity.HasOne(d => d.Vrsta)
                    .WithMany(p => p.MuzickaOprema)
                    .HasForeignKey(d => d.VrstaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VrstaId");
            });

            modelBuilder.Entity<Ocjene>(entity =>
            {
                entity.HasKey(e => e.OcjenaId)
                    .HasName("PK__Ocjene__E6FC7B49A040F860");

                entity.Property(e => e.OcjenaId).HasColumnName("OcjenaID");

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.HasOne(d => d.Klijent)
                    .WithMany(p => p.Ocjene)
                    .HasForeignKey(d => d.KlijentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KlijentId_Ocjena");

                entity.HasOne(d => d.MuzickaOprema)
                    .WithMany(p => p.Ocjene)
                    .HasForeignKey(d => d.MuzickaOpremaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MuzickaOprema_Ocjena");
            });

            modelBuilder.Entity<RezervacijaStavke>(entity =>
            {
                entity.HasKey(e => e.RezervacijaStavkaId)
                    .HasName("PK__Rezervac__D11E50DFFFF545FF");

                entity.HasOne(d => d.MuzickaOprema)
                    .WithMany(p => p.RezervacijaStavke)
                    .HasForeignKey(d => d.MuzickaOpremaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MuzickaOprema");

                entity.HasOne(d => d.Rezervacija)
                    .WithMany(p => p.RezervacijaStavke)
                    .HasForeignKey(d => d.RezervacijaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rezervacija");
            });

            modelBuilder.Entity<Rezervacije>(entity =>
            {
                entity.HasKey(e => e.RezervacijaId)
                    .HasName("PK__Rezervac__CABA44DD90634C31");

                entity.Property(e => e.BrojRezervacije)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.HasOne(d => d.Klijent)
                    .WithMany(p => p.Rezervacije)
                    .HasForeignKey(d => d.KlijentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KlijentId_Rezervacije");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Rezervacije)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KorisnikId_Rezervacije");
            });

            modelBuilder.Entity<RezervacijeGluveSobe>(entity =>
            {
                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.HasOne(d => d.Klijent)
                    .WithMany(p => p.RezervacijeGluveSobe)
                    .HasForeignKey(d => d.KlijentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KlijentId_GluvaSoba");

               
            });

            modelBuilder.Entity<Termini>(entity =>
            {
                entity.HasKey(e => e.TerminId)
                    .HasName("PK__Termini__42126C95AD36833F");

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Termini)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_KorisnikId_Termin");
            });

            modelBuilder.Entity<Uloge>(entity =>
            {
                entity.HasKey(e => e.UlogaId)
                    .HasName("PK__Uloge__DCAB23CB99CF9853");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Opis).HasMaxLength(200);
                entity.HasMany(d => d.Korisnici)
                    .WithOne(p => p.Uloga)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                   
            });

            modelBuilder.Entity<Vrsta>(entity =>
            {
                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            OnModelCreatingPartial(modelBuilder);
        }
       partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
