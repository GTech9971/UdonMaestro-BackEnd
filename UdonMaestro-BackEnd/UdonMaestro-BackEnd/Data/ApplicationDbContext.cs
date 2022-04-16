using Microsoft.EntityFrameworkCore;
using UdonMaestro_BackEnd.Data.Model;

namespace UdonMaestro_BackEnd.Data {
    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(DbContextOptions options) : base(options) {
        }

        public DbSet<Province> Provinces => Set<Province>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Town> Towns => Set<Town>();
        public DbSet<Shop> Shops => Set<Shop>();
    }
}
