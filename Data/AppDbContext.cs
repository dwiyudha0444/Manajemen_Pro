using Microsoft.EntityFrameworkCore;
using Manajemen_Pro.Models; // Sesuaikan dengan namespace model

namespace Manajemen_Pro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definisikan tabel dalam database
        public DbSet<Produk> Produk { get; set; }
    }
}