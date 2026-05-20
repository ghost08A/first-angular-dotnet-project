using Microsoft.AspNetCore.Mvc;
using back.DTOs;
using back.Services;

namespace back.Controllers
{
    // [ApiController] เป็นการบอก .NET ว่านี่คือ API นะ ไม่ใช่หน้าเว็บธรรมดา มันจะช่วยตรวจสอบข้อมูลเบื้องต้นให้
    [ApiController]
    [Route("api/[controller]")] // กำหนดเส้นทางของ API เป็น /api/auth (เพราะชื่อ Controller คือ AuthController)
     public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) // ฉีด Service เข้ามาใน Controller ผ่าน Constructor (Dependency Injection)
        {
            _authService = authService;
        }
        // URL เต็มๆ จะกลายเป็น: POST /api/auth/login
        [HttpPost("login")] // กำหนดว่าเมธอดนี้จะตอบสนองต่อ POST ที่ /api/auth/login
        public IActionResult Login([FromBody] LoginRequest request) //ชนิดของข้อมูลที่ฟังก์ชันนี้จะตอบกลับ (Return) แปลว่า "ฉันจะคืนค่าเป็นสถานะ HTTP พร้อมข้อมูลบางอย่าง" 
        {
            var token = _authService.Authenticate(request); // เรียกใช้ Service เพื่อเช็คข้อมูลและสร้าง Token
            if (token == null)
            {
                return Unauthorized(new { message = "Invalid email or password" }); // ถ้ารหัสผิด ให้ส่งสถานะ 401 พร้อมข้อความแจ้งเตือน
            }
            return Ok(new { token }); // ถ้ารหัสถูก ให้ส่งสถานะ 200 พร้อม Token กลับไป
    }
}   
}