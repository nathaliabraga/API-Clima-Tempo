using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimaSprint.Domain.Entities
{
    [Table("tb_previsao_clima")]
    public class PrevisaoClimaEntity
    {
        [Key]
        public int Id { get; set; }
        public string Cidade { get; set; } = string.Empty;
        public double Temperatura { get; set; }
        public string Condicao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    }
}
