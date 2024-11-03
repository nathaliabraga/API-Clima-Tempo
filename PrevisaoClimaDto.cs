using ClimaSprint.Domain.Interfaces.Dtos;
using FluentValidation;

namespace ClimaSprint.Application.Dtos
{
    public class PrevisaoClimaDto : IPrevisaoClimaDto
    {
        public string Cidade { get; set; } = string.Empty;
        public double Temperatura { get; set; }
        public string Condicao { get; set; } = string.Empty;
        public DateTime Data { get; set; }

        public void Validate()
        {
            var validateResult = new PrevisaoClimaDtoValidation().Validate(this);

            if (!validateResult.IsValid)
                throw new ArgumentException(string.Join(" e ", validateResult.Errors.Select(x => x.ErrorMessage)));
        }
    }

    internal class PrevisaoClimaDtoValidation : AbstractValidator<PrevisaoClimaDto>
    {
        public PrevisaoClimaDtoValidation()
        {
            RuleFor(x => x.Cidade)
                .MinimumLength(2).WithMessage("O nome da cidade deve ter no mínimo 2 caracteres")
                .NotEmpty().WithMessage("O nome da cidade não pode ser vazio");

            RuleFor(x => x.Temperatura)
                .InclusiveBetween(-100, 100).WithMessage("A temperatura deve estar entre -100 e 100 graus Celsius");

            RuleFor(x => x.Condicao)
                .NotEmpty().WithMessage("A condição climática não pode ser vazia");
        }
    }
}
