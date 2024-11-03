using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimaSprint.Domain.Entities
{
    [Table("tb_clima")]
    public class ClimaEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Cidade { get; set; } = string.Empty;

        [Required]
        public DateTime DataConsulta { get; set; }

        [Required]
        public double Temperatura { get; set; }

        [Required]
        public string Condicao { get; set; } = string.Empty;
    }
}
