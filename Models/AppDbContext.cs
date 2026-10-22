using Microsoft.EntityFrameworkCore;

namespace ql_cuahang.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Khai báo 5 bảng
        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

        // Khai báo khóa chính kép cho bảng ChiTietDonHang
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ChiTietDonHang>()
                .HasKey(c => new { c.MaDH, c.MaSP }); // Bảng này có 2 khóa chính
        }
    }
}