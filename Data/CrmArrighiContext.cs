using Microsoft.EntityFrameworkCore;
using CrmArrighi.Models;

namespace CrmArrighi.Data
{
    public class CrmArrighiContext : DbContext
    {
        public CrmArrighiContext(DbContextOptions<CrmArrighiContext> options)
            : base(options)
        {
        }

        public DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações para PessoaFisica
            modelBuilder.Entity<PessoaFisica>()
                .HasOne(p => p.Endereco)
                .WithMany()
                .HasForeignKey(p => p.EnderecoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PessoaFisica>()
                .HasIndex(p => p.Cpf)
                .IsUnique();

            modelBuilder.Entity<PessoaFisica>()
                .HasIndex(p => p.Email)
                .IsUnique();

            // Configurações para PessoaJuridica
            modelBuilder.Entity<PessoaJuridica>()
                .HasOne(p => p.Endereco)
                .WithMany()
                .HasForeignKey(p => p.EnderecoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PessoaJuridica>()
                .HasOne(p => p.ResponsavelTecnico)
                .WithMany()
                .HasForeignKey(p => p.ResponsavelTecnicoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PessoaJuridica>()
                .HasIndex(p => p.Cnpj)
                .IsUnique();

            modelBuilder.Entity<PessoaJuridica>()
                .HasIndex(p => p.Email)
                .IsUnique();

            // Configurações para Usuario
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Relacionamento Usuario com PessoaFisica
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.PessoaFisica)
                .WithMany()
                .HasForeignKey(u => u.PessoaFisicaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Usuario com PessoaJuridica
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.PessoaJuridica)
                .WithMany()
                .HasForeignKey(u => u.PessoaJuridicaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurações para Endereco
            modelBuilder.Entity<Endereco>()
                .Property(e => e.Cep)
                .HasMaxLength(9);
        }
    }
} 