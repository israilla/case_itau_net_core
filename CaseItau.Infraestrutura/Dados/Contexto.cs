using CaseItau.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;


namespace CaseItau.Infraestrutura.Dados
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }
        public DbSet<Fundo> Fundo { get; set; }
        public DbSet<TipoFundo> Tipo_Fundo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fundo>()
            .HasKey(f => f.Codigo);

            modelBuilder.Entity<TipoFundo>()
            .HasKey(t => t.Codigo);

            modelBuilder.Entity<Fundo>()
            .HasOne(f => f.Tipo_Fundo)
            .WithMany(t => t.Fundos)
            .HasForeignKey(f => f.Codigo_Tipo);

            base.OnModelCreating(modelBuilder);
        }
    }
}
