using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using back.DTOs;

namespace back.Services
{
    // Interface คือ 'แบบแปลน' ที่บอกว่า Service นี้ต้องมีฟังก์ชันอะไรบ้าง
    public interface IAuthService
    {
        string? Authenticate(LoginRequest request);
    }
   public class AuthService : IAuthService
    {
        // ใช้สำหรับดึงค่าความลับ (Secret Key) จากไฟล์ appsettings.json
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)    //เป็นการฉีดค่าคอนฟิกเข้ามาใน Service ผ่าน Constructor (Dependency Injection)
        {
            _configuration = configuration;
        }
        public string? Authenticate(LoginRequest request)
        {
            // จำลองการเช็ค Database (รหัสที่เราตกลงกันไว้)
            if (request.Email != "tast@gmail.com" || request.Password != "tast123?")
            {
                return null; // ถ้ารหัสผิด ให้คืนค่า null กลับไป (แปลว่าไม่ได้ Token)
            }

            // ถ้ารหัสถูก ให้สร้างบัตรผ่าน JWT
            return GenerateJwtToken(request.Email);
        }

        // ฟังก์ชันสร้าง Token
        private string GenerateJwtToken(string email)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings"); 
            var secretKey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]!); // ดึงค่าความลับ (Secret Key) จากไฟล์คอนฟิก และแปลงเป็น byte array

            // สิ่งที่เราจะฝังไปในบัตรผ่าน เช่น อีเมล, ไอดีผู้ใช้ (เราเรียกสิ่งนี้ว่า Claims)
            var claims = new[] 
            { 
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, "1"),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),//  เอาข้อมูล Claims มาใส่
                // บัตรผ่านหมดอายุในกี่นาที
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"]!)),
                Issuer = jwtSettings["Issuer"], //ใครเป็นคนออกบัตร (ป้องกันคนเอา Token ที่อื่นมาสวมรอย)
                Audience = jwtSettings["Audience"], //บัตรนี้ทำมาเพื่อใคร
                // เซ็นกำกับบัตรผ่านด้วยกุญแจลับ เพื่อป้องกันการปลอมแปลง
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        
    }
}