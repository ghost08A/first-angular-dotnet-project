namespace back.DTOs
{
    // คลาสนี้เอาไว้ "รับ" ข้อมูลมาจาก Angular
    // ชื่อตัวแปร (Properties) ควรจะตั้งให้ตรงหรือล้อกับที่ Angular ส่งมา
    public class LoginRequest
    {
        // required แปลว่า "ห้ามเป็นค่าว่าง" (ห้ามส่ง null มา)
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}