using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ql_cuahang.Models
{
    [Table("DonHang")]
    public class DonHang
    {
        [Key]
        public int MaDH { get; set; }
        public DateTime NgayDat { get; set; } = DateTime.Now;
        public decimal TongTien { get; set; } = 0;
        public int TrangThaiDH { get; set; } = 0;

        public int MaKH { get; set; }
        [ForeignKey("MaKH")]
        public KhachHang? KhachHang { get; set; }
    }
}