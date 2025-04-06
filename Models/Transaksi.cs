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
