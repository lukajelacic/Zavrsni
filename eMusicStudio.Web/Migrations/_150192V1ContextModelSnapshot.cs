﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eMusicStudio.Web.Models;

namespace eMusicStudio.Web.Migrations
{
    [DbContext(typeof(_150192V1Context))]
    partial class _150192V1ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eMusicStudio.Web.Models.Grad", b =>
                {
                    b.Property<int>("GradId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GradId");

                    b.ToTable("Grad");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Klijenti", b =>
                {
                    b.Property<int>("KlijentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Banovan")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("GradId")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("KorisnickoIme")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LozinkaHash")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LozinkaSalt")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Slika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("KlijentId")
                        .HasName("PK__Klijenti__5F05D8AEEDF14F69");

                    b.HasIndex("GradId");

                    b.ToTable("Klijenti");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Korisnici", b =>
                {
                    b.Property<int>("KorisnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("KorisnickoIme")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LozinkaHash")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LozinkaSalt")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefon")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UlogaId")
                        .HasColumnType("int");

                    b.HasKey("KorisnikId")
                        .HasName("PK__Korisnic__80B06D41F5FF910E");

                    b.HasIndex("UlogaId");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.MuzickaOprema", b =>
                {
                    b.Property<int>("MuzickaOpremaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cijena")
                        .HasColumnType("int");

                    b.Property<int>("NaStanju")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Slika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VrstaId")
                        .HasColumnType("int");

                    b.HasKey("MuzickaOpremaId");

                    b.HasIndex("VrstaId");

                    b.ToTable("MuzickaOprema");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Ocjene", b =>
                {
                    b.Property<int>("OcjenaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OcjenaID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime");

                    b.Property<int>("KlijentId")
                        .HasColumnType("int");

                    b.Property<int>("MuzickaOpremaId")
                        .HasColumnType("int");

                    b.Property<int>("Ocjena")
                        .HasColumnType("int");

                    b.HasKey("OcjenaId")
                        .HasName("PK__Ocjene__E6FC7B49A040F860");

                    b.HasIndex("KlijentId");

                    b.HasIndex("MuzickaOpremaId");

                    b.ToTable("Ocjene");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.RezervacijaStavke", b =>
                {
                    b.Property<int>("RezervacijaStavkaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<int>("MuzickaOpremaId")
                        .HasColumnType("int");

                    b.Property<int>("RezervacijaId")
                        .HasColumnType("int");

                    b.HasKey("RezervacijaStavkaId")
                        .HasName("PK__Rezervac__D11E50DFFFF545FF");

                    b.HasIndex("MuzickaOpremaId");

                    b.HasIndex("RezervacijaId");

                    b.ToTable("RezervacijaStavke");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Rezervacije", b =>
                {
                    b.Property<int>("RezervacijaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Arhivirana")
                        .HasColumnType("bit");

                    b.Property<int>("BrojRezervacije")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<decimal?>("Cijena")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DatumRezervacije")
                        .HasColumnType("datetime2");

                    b.Property<int>("KlijentId")
                        .HasColumnType("int");

                    b.Property<int?>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<bool?>("Otkazano")
                        .HasColumnType("bit");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("RezervacijaId")
                        .HasName("PK__Rezervac__CABA44DD90634C31");

                    b.HasIndex("KlijentId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Rezervacije");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.RezervacijeGluveSobe", b =>
                {
                    b.Property<int>("RezervacijeGluveSobeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime");

                    b.Property<int>("KlijentId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("VrijemeDo")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("VrijemeOd")
                        .HasColumnType("time");

                    b.HasKey("RezervacijeGluveSobeId");

                    b.HasIndex("KlijentId");

                    b.ToTable("RezervacijeGluveSobe");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Termini", b =>
                {
                    b.Property<int>("TerminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Aktivan")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("VrijemeDo")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("VrijemeOd")
                        .HasColumnType("time");

                    b.HasKey("TerminId")
                        .HasName("PK__Termini__42126C95AD36833F");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Termini");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Uloge", b =>
                {
                    b.Property<int>("UlogaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Opis")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UlogaId")
                        .HasName("PK__Uloge__DCAB23CB99CF9853");

                    b.ToTable("Uloge");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Vrsta", b =>
                {
                    b.Property<int>("VrstaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("VrstaId");

                    b.ToTable("Vrsta");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Klijenti", b =>
                {
                    b.HasOne("eMusicStudio.Web.Models.Grad", "Grad")
                        .WithMany("Klijenti")
                        .HasForeignKey("GradId")
                        .HasConstraintName("FK_GradId")
                        .IsRequired();

                    b.Navigation("Grad");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Korisnici", b =>
                {
                    b.HasOne("eMusicStudio.Web.Models.Uloge", "Uloga")
                        .WithMany("Korisnici")
                        .HasForeignKey("UlogaId")
                        .HasConstraintName("FK_UlogaId_Korisnici")
                        .IsRequired();

                    b.Navigation("Uloga");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.MuzickaOprema", b =>
                {
                    b.HasOne("eMusicStudio.Web.Models.Vrsta", "Vrsta")
                        .WithMany("MuzickaOprema")
                        .HasForeignKey("VrstaId")
                        .HasConstraintName("FK_VrstaId")
                        .IsRequired();

                    b.Navigation("Vrsta");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Ocjene", b =>
                {
                    b.HasOne("eMusicStudio.Web.Models.Klijenti", "Klijent")
                        .WithMany("Ocjene")
                        .HasForeignKey("KlijentId")
                        .HasConstraintName("FK_KlijentId_Ocjena")
                        .IsRequired();

                    b.HasOne("eMusicStudio.Web.Models.MuzickaOprema", "MuzickaOprema")
                        .WithMany("Ocjene")
                        .HasForeignKey("MuzickaOpremaId")
                        .HasConstraintName("FK_MuzickaOprema_Ocjena")
                        .IsRequired();

                    b.Navigation("Klijent");

                    b.Navigation("MuzickaOprema");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.RezervacijaStavke", b =>
                {
                    b.HasOne("eMusicStudio.Web.Models.MuzickaOprema", "MuzickaOprema")
                        .WithMany("RezervacijaStavke")
                        .HasForeignKey("MuzickaOpremaId")
                        .HasConstraintName("FK_MuzickaOprema")
                        .IsRequired();

                    b.HasOne("eMusicStudio.Web.Models.Rezervacije", "Rezervacija")
                        .WithMany("RezervacijaStavke")
                        .HasForeignKey("RezervacijaId")
                        .HasConstraintName("FK_Rezervacija")
                        .IsRequired();

                    b.Navigation("MuzickaOprema");

                    b.Navigation("Rezervacija");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Rezervacije", b =>
                {
                    b.HasOne("eMusicStudio.Web.Models.Klijenti", "Klijent")
                        .WithMany("Rezervacije")
                        .HasForeignKey("KlijentId")
                        .HasConstraintName("FK_KlijentId_Rezervacije")
                        .IsRequired();

                    b.HasOne("eMusicStudio.Web.Models.Korisnici", "Korisnik")
                        .WithMany("Rezervacije")
                        .HasForeignKey("KorisnikId")
                        .HasConstraintName("FK_KorisnikId_Rezervacije");

                    b.Navigation("Klijent");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.RezervacijeGluveSobe", b =>
                {
                    b.HasOne("eMusicStudio.Web.Models.Klijenti", "Klijent")
                        .WithMany("RezervacijeGluveSobe")
                        .HasForeignKey("KlijentId")
                        .HasConstraintName("FK_KlijentId_GluvaSoba")
                        .IsRequired();

                    b.Navigation("Klijent");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Termini", b =>
                {
                    b.HasOne("eMusicStudio.Web.Models.Korisnici", "Korisnik")
                        .WithMany("Termini")
                        .HasForeignKey("KorisnikId")
                        .HasConstraintName("FK_KorisnikId_Termin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Grad", b =>
                {
                    b.Navigation("Klijenti");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Klijenti", b =>
                {
                    b.Navigation("Ocjene");

                    b.Navigation("Rezervacije");

                    b.Navigation("RezervacijeGluveSobe");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Korisnici", b =>
                {
                    b.Navigation("Rezervacije");

                    b.Navigation("Termini");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.MuzickaOprema", b =>
                {
                    b.Navigation("Ocjene");

                    b.Navigation("RezervacijaStavke");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Rezervacije", b =>
                {
                    b.Navigation("RezervacijaStavke");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Uloge", b =>
                {
                    b.Navigation("Korisnici");
                });

            modelBuilder.Entity("eMusicStudio.Web.Models.Vrsta", b =>
                {
                    b.Navigation("MuzickaOprema");
                });
#pragma warning restore 612, 618
        }
    }
}
