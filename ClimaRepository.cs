using ClimaSprint.Data.AppData;
using ClimaTempo.Domain.Entities;
using ClimaSprint.Domain.Interfaces;

namespace ClimaSprint.Data.Repositories
{
    public class ClimaRepository : IClimaRepository
    {
        private readonly ApplicationContext _context;

        public ClimaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public ClimaEntity? Adicionar(ClimaEntity clima)
        {
            _context.Clima.Add(clima);
            _context.SaveChanges();

            return clima;
        }

        public ClimaEntity? Editar(ClimaEntity clima)
        {
            var entity = _context.Clima.Find(clima.Id);

            if (entity is not null)
            {
                entity.Cidade = clima.Cidade;
                entity.DataConsulta = clima.DataConsulta;
                entity.Temperatura = clima.Temperatura;
                entity.Condicao = clima.Condicao;

                _context.Clima.Update(entity);
                _context.SaveChanges();
            }
            return entity;
        }

        public ClimaEntity? ObterPorId(int id)
        {
            var entity = _context.Clima.Find(id);

            return entity;
        }

        public IEnumerable<ClimaEntity> ObterTodos()
        {
            var entity = _context.Clima.ToList();

            return entity;
        }

        public ClimaEntity? Remover(int id)
        {
            var entity = _context.Clima.Find(id);

            if (entity is not null)
            {
                _context.Clima.Remove(entity);
                _context.SaveChanges();

                return entity;
            }
            return null;
        }
    }
}
