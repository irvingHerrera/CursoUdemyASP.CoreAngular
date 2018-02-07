using CursoUdemy.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace CursoUdemy.Persistence
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features { get; set; }

    }
}
