using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CaseItau.Dominio.Entidades
{
    public class Fundo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public int Codigo_Tipo { get; set; }
        public decimal? Patrimonio { get; set; }
        public TipoFundo Tipo_Fundo { get; set; }
    }
}
