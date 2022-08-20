using Microsoft.EntityFrameworkCore;
using MinhaApi.Models;

namespace MinhaApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions? options) : base(options!)
        {
        }

        public DbSet<AnimalsModel>? Animal {get; set; }
        public DbSet<RacaModel>? Racas {get; set; }
    }
}