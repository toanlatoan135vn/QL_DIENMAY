using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ql_cuahang.Models;

namespace ql_cuahang.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly AppDbContext _context;

        public SanPhamController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Khách hàng: Danh sách + Lọc theo loại
        public async Task<IActionResult> Index(int? maloai)
        {
            ViewBag.LoaiList = await _context.LoaiSanPhams.ToListAsync();
            ViewBag.CurrentMaLoai = maloai;

            var query = _context.SanPhams.Include(s => s.LoaiSanPham).AsQueryable();
            if (maloai.HasValue)
            {
                query = query.Where(s => s.MaLoai == maloai.Value);
            }
            return View(await query.ToListAsync());
        }

        // 2. Khách hàng: Chi tiết + 4 sản phẩm liên quan
        public async Task<IActionResult> Details(int id)
        {
            var sanPham = await _context.SanPhams
                .Include(s => s.LoaiSanPham)
                .FirstOrDefaultAsync(s => s.MaSP == id);

            if (sanPham == null) return NotFound();

            ViewBag.SanPhamLienQuan = await _context.SanPhams
                .Where(s => s.MaLoai == sanPham.MaLoai && s.MaSP != id)
                .Take(4)
                .ToListAsync();

            return View(sanPham);
        }

        // 3. Admin: Bảng quản lý sản phẩm
        public async Task<IActionResult> AdminIndex()
        {
            var ds = await _context.SanPhams.Include(s => s.LoaiSanPham).ToListAsync();
            return View("~/Areas/Admin/Views/SanPham/Index.cshtml", ds);
        }

        // Thêm (GET)
        public IActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(_context.LoaiSanPhams, "MaLoai", "TenLoai");
            return View("~/Areas/Admin/Views/SanPham/Create.cshtml");
        }

        // Thêm (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham sanPham, IFormFile? fileHinh)
        {
            if (fileHinh != null && fileHinh.Length > 0)
            {
                string fileName = Path.GetFileName(fileHinh.FileName);
                string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                Directory.CreateDirectory(imagesFolder);

                string filePath = Path.Combine(imagesFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileHinh.CopyToAsync(stream);
                }
                sanPham.HinhAnh = fileName;
            }

            _context.Add(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminIndex));
        }

        // Sửa (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null) return NotFound();

            ViewBag.MaLoai = new SelectList(_context.LoaiSanPhams, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View("~/Areas/Admin/Views/SanPham/Edit.cshtml", sanPham);
        }

        // Sửa (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SanPham sanPham, IFormFile? fileHinh)
        {
            if (id != sanPham.MaSP) return NotFound();

            var spDb = await _context.SanPhams.AsNoTracking().FirstOrDefaultAsync(s => s.MaSP == id);
            if (spDb == null) return NotFound();

            if (fileHinh != null && fileHinh.Length > 0)
            {
                string fileName = Path.GetFileName(fileHinh.FileName);
                string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                Directory.CreateDirectory(imagesFolder);

                string filePath = Path.Combine(imagesFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileHinh.CopyToAsync(stream);
                }
                sanPham.HinhAnh = fileName;
            }
            else
            {
                sanPham.HinhAnh = spDb.HinhAnh;
            }

            _context.Update(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminIndex));
        }

        // Xóa
        public async Task<IActionResult> Delete(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AdminIndex));
        }
    }
}