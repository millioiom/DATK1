using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongTro.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã phòng")]
        [StringLength(20, ErrorMessage = "Mã phòng tối đa 20 ký tự")]
        public string RoomCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập diện tích")]
        [Range(1, 1000, ErrorMessage = "Diện tích phải từ 1 đến 1000 m²")]
        public decimal Area { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá thuê")]
        [Range(0, 100000000, ErrorMessage = "Giá thuê không hợp lệ")]
        public decimal Price { get; set; }

        [StringLength(100)]
        public string? TenantName { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái")]
        public string Status { get; set; } = "Còn trống";
    }
}