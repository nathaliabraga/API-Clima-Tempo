using ClimaTempo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClimaTempo.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<ClimaEntity> Clima { get; set; }
    }
}
