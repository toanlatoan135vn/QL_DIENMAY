using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ql_cuahang.Models
{
    [Table("LoaiSanPham")]
    public class LoaiSanPham
    {
        [Key]
        public int MaLoai { get; set; }
        [Required]
        public string TenLoai { get; set; } = string.Empty;

        public List<SanPham>? SanPhams { get; set; }
    }
}