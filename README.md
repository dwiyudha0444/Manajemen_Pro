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

# 4. Membuat dan Menjalankan Migrasi
dotnet ef migrations add InitialCreate
dotnet ef database update

# 5. Menjalankan Aplikasi
 dotnet run 
 dotnet watch run
