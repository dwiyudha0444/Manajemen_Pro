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
