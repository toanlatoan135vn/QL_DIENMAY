using System.ComponentModel.DataAnnotations.Schema;

namespace ql_cuahang.Models
{
    [Table("ChiTietDonHang")]
    public class ChiTietDonHang
    {
        public int MaDH { get; set; }
        [ForeignKey("MaDH")]
        public DonHang? DonHang { get; set; }

        public int MaSP { get; set; }
        [ForeignKey("MaSP")]
        public SanPham? SanPham { get; set; }

        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }
}