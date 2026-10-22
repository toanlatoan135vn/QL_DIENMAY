using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ql_cuahang.Models
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        public int MaKH { get; set; }
        [Required]
        public string HoTen { get; set; } = string.Empty;
        [Required]
        public string TaiKhoan { get; set; } = string.Empty;
        [Required]
        public string MatKhau { get; set; } = string.Empty;
        public string? DienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public bool LaAdmin { get; set; } = false;
    }
}