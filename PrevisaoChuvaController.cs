using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace ClimaSprint.Presentation.Controllers
{
    public class DadosPrevisaoChuva
    {
        [LoadColumn(0)] public DateTime Data { get; set; }
        [LoadColumn(1)] public float Temperatura { get; set; }
        [LoadColumn(2)] public float Umidade { get; set; }
        [LoadColumn(3)] public float Pressao { get; set; }
        [LoadColumn(4)] public float VelocidadeVento { get; set; }
        [LoadColumn(5)] public string CondicaoClimatologica { get; set; }
        [LoadColumn(6)] public float Precipitacao { get; set; }
        [LoadColumn(7)] public bool Choveu { get; set; } 
    }

    public class PrevisaoChuva
    {
        [ColumnName("PredictedLabel")]
        public bool Choveu { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PrevisaoChuvaController : ControllerBase
    {
        private readonly string caminhoModelo = Path.Combine(Environment.CurrentDirectory, "wwwroot", "MLModels", "ModeloPrevisaoChuva.zip");
        private readonly string caminhoTreinamento = Path.Combine(Environment.CurrentDirectory, "Data", "PrevisaoChuva.csv");
        private readonly MLContext mlContext;

        public PrevisaoChuvaController()
        {
            mlContext = new MLContext();

            if (!System.IO.File.Exists(caminhoModelo))
            {
                Console.WriteLine("Modelo não encontrado. Iniciando treinamento...");
                TreinarModelo();
            }
        }

        [HttpGet("prever-chuva")]
        public IActionResult PreverChuva(float temperatura, float umidade, float pressao, float velocidadeVento, string condicaoClimatologica, float precipitacao)
        {
            if (!System.IO.File.Exists(caminhoModelo))
            {
                return BadRequest("O modelo ainda não foi treinado.");
            }

            ITransformer modelo;
            using (var stream = new FileStream(caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                modelo = mlContext.Model.Load(stream, out var modeloSchema);
            }

            var enginePrevisao = mlContext.Model.CreatePredictionEngine<DadosPrevisaoChuva, PrevisaoChuva>(modelo);

            var previsao = enginePrevisao.Predict(new DadosPrevisaoChuva
            {
                Temperatura = temperatura,
                Umidade = umidade,
                Pressao = pressao,
                VelocidadeVento = velocidadeVento,
                CondicaoClimatologica = condicaoClimatologica,
                Precipitacao = precipitacao
            });

            return Ok(new
            {
                Choveu = previsao.Choveu
            });
        }

        private void TreinarModelo()
        {
            var pastaModelo = Path.GetDirectoryName(caminhoModelo);
            if (!Directory.Exists(pastaModelo))
            {
                Directory.CreateDirectory(pastaModelo);
                Console.WriteLine($"Diretório criado: {pastaModelo}");
            }

            IDataView dadosTreinamento = mlContext.Data.LoadFromTextFile<DadosPrevisaoChuva>(
                path: caminhoTreinamento, hasHeader: true, separatorChar: ',');

            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(DadosPrevisaoChuva.Choveu))
                .Append(mlContext.Transforms.Concatenate("Features", nameof(DadosPrevisaoChuva.Temperatura),
                                                          nameof(DadosPrevisaoChuva.Umidade),
                                                          nameof(DadosPrevisaoChuva.Pressao),
                                                          nameof(DadosPrevisaoChuva.VelocidadeVento),
                                                          nameof(DadosPrevisaoChuva.CondicaoClimatologica),
                                                          nameof(DadosPrevisaoChuva.Precipitacao)))
                .Append(mlContext.BinaryClassification.Trainers.FastTree());

            var modelo = pipeline.Fit(dadosTreinamento);

            mlContext.Model.Save(modelo, dadosTreinamento.Schema, caminhoModelo);
            Console.WriteLine($"Modelo treinado e salvo em: {caminhoModelo}");
        }
    }
}
