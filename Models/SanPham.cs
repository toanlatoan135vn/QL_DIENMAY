using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ql_cuahang.Models
{
    [Table("SanPham")]
    public class SanPham
    {
        [Key]
        public int MaSP { get; set; }

        [Required]
        public string TenSP { get; set; } = string.Empty;

        [Required]
        public decimal GiaBan { get; set; }

        public string? HinhAnh { get; set; }

        public bool TrangThai { get; set; } = true;

        public bool SanPhamNoiBat { get; set; }

        [Required]
        public int MaLoai { get; set; }

        [ForeignKey("MaLoai")]
        public LoaiSanPham? LoaiSanPham { get; set; }
    }
}