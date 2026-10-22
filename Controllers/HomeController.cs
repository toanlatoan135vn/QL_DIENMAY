using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ql_cuahang.Models; 

namespace ql_cuahang.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        // Tiêm AppDbContext vào để sử dụng Database
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Lấy toàn bộ sản phẩm trong SQL ra
            var sanphams = _context.SanPhams.ToList();

            // Truyền danh sách đó ra View (Giao diện)
            return View(sanphams);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}