using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Manajemen_Pro.Models;
using Manajemen_Pro.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Manajemen_Produk.Controllers
{
    [Route("transaksi")]
    public class TransaksiController : Controller
    {
        private readonly AppDbContext _context;

        public TransaksiController(AppDbContext context)
        {
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var transaksi = await _context.Transaksi.ToListAsync();
            return View(transaksi);
        }

        // Menampilkan Form Create
        [HttpGet]
        [Route("tambah")] // URL: /transaksi/tambah
        public IActionResult Create()
        {
            // Kirim daftar produk ke View
            ViewBag.ProdukList = new SelectList(_context.Produk, "Id", "Nama");
            return View();
        }

        [HttpPost]
        [Route("tambah")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NamaPelanggan,ProdukId,Jumlah,TotalHarga,Tanggal")] Transaksi transaksi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaksi);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Kirim ulang data produk jika validasi gagal
            ViewBag.ProdukList = new SelectList(_context.Produk, "Id", "Nama", transaksi.ProdukId);
            return View(transaksi);
        }

        // Konfirmasi Hapus Transaksi
        [HttpGet]
        [Route("hapus/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transaksi = await _context.Transaksi
                .Include(t => t.Produk) // => ini yang penting
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaksi == null)
            {
                return NotFound();
            }

            return View(transaksi); // Kirim transaksi lengkap dengan relasi Produk
        }


        // Proses Hapus Transaksi
        [HttpPost, ActionName("Delete")]
        [Route("hapus/{id}")]
        [ValidateAntiForgeryToken] // Mencegah serangan CSRF
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaksi = await _context.Transaksi.FindAsync(id);
            if (transaksi != null)
            {
                _context.Transaksi.Remove(transaksi); // Hapus transaksi dari database
                await _context.SaveChangesAsync(); // Simpan perubahan
            }
            return RedirectToAction("Index"); // Kembali ke daftar transaksi
        }

        // GET: Transaksi/Edit/5
        [HttpGet]
        [Route("Transaksi/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var transaksi = await _context.Transaksi.FindAsync(id);
            if (transaksi == null)
            {
                return NotFound();
            }

            // Kirim daftar produk ke dropdown
            ViewBag.ProdukList = new SelectList(_context.Produk, "Id", "Nama", transaksi.ProdukId);
            return View(transaksi);
        }

        // POST: Transaksi/Edit/5
        [HttpPost]
        [Route("Transaksi/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaPelanggan,ProdukId,Jumlah,TotalHarga,Tanggal")] Transaksi transaksi)
        {
            if (id != transaksi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaksi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaksiExists(transaksi.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Jika gagal, isi ulang produk
            ViewBag.ProdukList = new SelectList(_context.Produk, "Id", "Nama", transaksi.ProdukId);
            return View(transaksi);
        }

        private bool TransaksiExists(int id)
        {
            return _context.Transaksi.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaksi = await _context.Transaksi
                .Include(t => t.Produk) // ← Menyertakan relasi ke Produk
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaksi == null)
            {
                return NotFound();
            }

            return View(transaksi); // Mengirim ke View
        }


    }
}