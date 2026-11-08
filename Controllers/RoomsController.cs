using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Models;

namespace QuanLyPhongTro.Controllers
{
    public class RoomsController : Controller
    {
        // Danh sách phòng mẫu
        private static readonly List<Room> _rooms = new()
        {
            new Room
            {
                Id = 1,
                RoomCode = "P101",
                Area = 20,
                Price = 2500000,
                TenantName = "Nguyễn Văn A",
                Status = "Đang thuê"
            },

            new Room
            {
                Id = 2,
                RoomCode = "P102",
                Area = 18,
                Price = 2000000,
                TenantName = null,
                Status = "Còn trống"
            },

            new Room
            {
                Id = 3,
                RoomCode = "P103",
                Area = 25,
                Price = 3000000,
                TenantName = "Trần Văn B",
                Status = "Đang thuê"
            },

            new Room
            {
                Id = 4,
                RoomCode = "P104",
                Area = 22,
                Price = 2200000,
                TenantName = null,
                Status = "Còn trống"
            },

            new Room
            {
                Id = 5,
                RoomCode = "P105",
                Area = 30,
                Price = 3500000,
                TenantName = "Lê Văn C",
                Status = "Đang thuê"
            }
        };

        // Hiển thị danh sách phòng
        public IActionResult Index()
        {
            return View(_rooms);
        }

        // Mở trang thêm phòng
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Room());
        }

        // Xử lý khi nhấn nút Lưu phòng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room room)
        {
            room.RoomCode = room.RoomCode?.Trim() ?? "";
            room.TenantName = room.TenantName?.Trim();

            // Kiểm tra mã phòng trùng
            if (!string.IsNullOrWhiteSpace(room.RoomCode) &&
                _rooms.Any(r => r.RoomCode.Equals(
                    room.RoomCode,
                    StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(
                    "RoomCode",
                    "Mã phòng này đã tồn tại!"
                );
            }

            // Phòng đang thuê phải có tên người thuê
            if (room.Status == "Đang thuê" &&
                string.IsNullOrWhiteSpace(room.TenantName))
            {
                ModelState.AddModelError(
                    "TenantName",
                    "Vui lòng nhập tên người thuê!"
                );
            }

            // Phòng trống không có người thuê
            if (room.Status == "Còn trống")
            {
                room.TenantName = null;
            }

            if (!ModelState.IsValid)
            {
                return View(room);
            }

            // Tạo ID tự động
            room.Id = _rooms.Count == 0
                ? 1
                : _rooms.Max(r => r.Id) + 1;

            // Lưu vào danh sách
            _rooms.Add(room);

            TempData["SuccessMessage"] = "Thêm phòng thành công!";

            return RedirectToAction("Index");
        }
    }
}