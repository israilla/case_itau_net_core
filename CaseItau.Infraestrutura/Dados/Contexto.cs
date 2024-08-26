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
        public DbSet<Fundo> Fundos { get; set; }
        public DbSet<TipoFundo> TiposFundos { get; set; }
    }
}
