using AbrigoHub.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbrigoHub.Infrastructure.Data
{
    public class AbrigoHubContext : DbContext
    {
        public AbrigoHubContext(DbContextOptions<AbrigoHubContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Abrigo> Abrigos { get; set; }
        public DbSet<AbrigoNecessidade> AbrigosNecessidades { get; set; }
        public DbSet<AbrigoRecurso> AbrigosRecursos { get; set; }
        public DbSet<Doacao> Doacoes { get; set; }
        public DbSet<AbrigoAvaliacao> AbrigosAvaliacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações do Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SenhaHash).IsRequired();
                entity.Property(e => e.TipoUsuario).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Telefone).HasMaxLength(20);
            });

            // Configurações do Abrigo
            modelBuilder.Entity<Abrigo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Endereco).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Cidade).HasMaxLength(100);
                entity.Property(e => e.Estado).HasMaxLength(50);
                entity.Property(e => e.Cep).HasMaxLength(20);
                entity.Property(e => e.Status).HasMaxLength(30).HasDefaultValue("aberto");

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Abrigos)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configurações do AbrigoNecessidade
            modelBuilder.Entity<AbrigoNecessidade>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TipoRecurso).IsRequired().HasMaxLength(50);
                entity.Property(e => e.NivelUrgencia).HasMaxLength(20).HasDefaultValue("normal");

                entity.HasOne(e => e.Abrigo)
                    .WithMany(a => a.Necessidades)
                    .HasForeignKey(e => e.AbrigoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configurações do AbrigoRecurso
            modelBuilder.Entity<AbrigoRecurso>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TipoRecurso).IsRequired().HasMaxLength(50);

                entity.HasOne(e => e.Abrigo)
                    .WithMany(a => a.Recursos)
                    .HasForeignKey(e => e.AbrigoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configurações da Doacao
            modelBuilder.Entity<Doacao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NomeDoador).HasMaxLength(100);
                entity.Property(e => e.TipoRecurso).IsRequired().HasMaxLength(50);

                entity.HasOne(e => e.Abrigo)
                    .WithMany(a => a.Doacoes)
                    .HasForeignKey(e => e.AbrigoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configurações do AbrigoAvaliacao
            modelBuilder.Entity<AbrigoAvaliacao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Avaliacao).IsRequired();

                entity.HasOne(e => e.Abrigo)
                    .WithMany(a => a.Avaliacoes)
                    .HasForeignKey(e => e.AbrigoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Avaliacoes)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
} 