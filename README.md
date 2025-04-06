# Manajemen_Pro

Aplikasi manajemen produk dan transaksi menggunakan ASP.NET Core MVC + Entity Framework Core.

## 🚀 Setup Awal

Berikut adalah langkah-langkah untuk memulai proyek ini dari awal:

```bash
# 1. Membuat project baru
dotnet new mvc -n Manajemen_Pro --framework net9.0
cd Manajemen_Pro

# 2. Install Entity Framework Core dan provider MySQL (Pomelo)
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.5
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.5
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.5
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0

# 3. Membuat folder untuk menyimpan file database atau context
mkdir data

# 4. AppDbContext

using Microsoft.EntityFrameworkCore;
using Manajemen_Pro.Models; 

namespace Manajemen_Pro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definisikan tabel dalam database
        public DbSet<Produk> Produk { get; set; }
        public DbSet<Transaksi> Transaksi { get; set; }


    }
}

# 5. Data/Model

namespace Manajemen_Pro.Models
{
    public class Produk
    {
        public int Id { get; set; }
        public string? Nama { get; set; }
        public int? Harga { get; set; }

        // Relasi: satu produk bisa punya banyak transaksi
        public List<Transaksi>? Transaksis { get; set; }
    }
}

using System;

namespace Manajemen_Pro.Models
{
    public class Transaksi
    {
        public int Id { get; set; }
        public DateTime Tanggal { get; set; }
        public string? NamaPelanggan { get; set; }
        public int? Jumlah { get; set; }

        public decimal? TotalHarga { get; set; }

        // Relasi ke Produk
        public int ProdukId { get; set; }
        public Produk? Produk { get; set; }
    }
}


# 6. appsettings.json

"ConnectionStrings": {
  "DefaultConnection": "server=127.0.0.1;database=mydb;user=root;password=;TreatTinyAsBoolean=false"
}

# 7. Program.cs

using Microsoft.EntityFrameworkCore;
using Manajemen_Pro.Data;

var builder = WebApplication.CreateBuilder(args);

// Konfigurasi Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Tambahkan layanan MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

# 8. Membuat dan Menjalankan Migrasi
dotnet ef migrations add InitialCreate
dotnet ef database update

# 9. Menjalankan Aplikasi
 dotnet run 
 dotnet watch run
