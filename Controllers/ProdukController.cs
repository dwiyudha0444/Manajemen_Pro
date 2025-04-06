using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Manajemen_Pro.Models;
using Manajemen_Pro.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Manajemen_Pro.Controllers
{

    [Route("produk")]
    public class ProdukController : Controller
    {
        private readonly AppDbContext _context;

        public ProdukController(AppDbContext context)
        {
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var produk = await _context.Produk.ToListAsync();
            return View(produk);
        }

        // Menampilkan Form Create
        [HttpGet]
        [Route("tambah")] // URL: /produk/tambah
        public IActionResult Create()
        {
            return View();
        }

        // Menyimpan Data ke Database
        [HttpPost]
        [Route("tambah")]
        [ValidateAntiForgeryToken] // Mencegah serangan CSRF
        public async Task<IActionResult> Create([Bind("Nama,Harga")] Produk produk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produk); // Tambahkan ke database
                await _context.SaveChangesAsync(); // Simpan perubahan
                return RedirectToAction("Index"); // Kembali ke daftar produk
            }
            return View(produk);
        }

        // Konfirmasi Hapus Produk
        [HttpGet]
        [Route("hapus/{id}")] // URL: /produk/hapus/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var produk = await _context.Produk.FindAsync(id);
            if (produk == null)
            {
                return NotFound();
            }
            return View(produk); // Menampilkan konfirmasi penghapusan
        }

        // Proses Hapus Produk
        [HttpPost, ActionName("Delete")]
        [Route("hapus/{id}")]
        [ValidateAntiForgeryToken] // Mencegah serangan CSRF
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produk = await _context.Produk.FindAsync(id);
            if (produk != null)
            {
                _context.Produk.Remove(produk); // Hapus produk dari database
                await _context.SaveChangesAsync(); // Simpan perubahan
            }
            return RedirectToAction("Index"); // Kembali ke daftar produk
        }

        // GET: Product/Edit/5
        [HttpGet]
        [Route("Produk/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var produk = await _context.Produk.FindAsync(id);
            if (produk == null)
            {
                return NotFound();
            }
            return View(produk);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [Route("Produk/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Nama, Harga")] Produk produk)
        {
            if (id != produk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produk);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(produk.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(produk);
        }

        [HttpGet]
        [Route("detail/{id}")]
        
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produk = await _context.Produk.FirstOrDefaultAsync(p => p.Id == id);

            if (produk == null)
            {
                return NotFound();
            }

            return View(produk); // ← Kirim satu objek Produk
        }



        private bool ProductExists(int id)
        {
            return _context.Produk.Any(e => e.Id == id);
        }

    }

}
