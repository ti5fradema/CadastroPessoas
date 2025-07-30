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

            // Configurações para Endereco
            modelBuilder.Entity<Endereco>()
                .Property(e => e.Cep)
                .HasMaxLength(9);
        }
    }
} 