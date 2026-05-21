using back.DTOs;

namespace back.Services
{
    //ไฟลฒนี้ทำหน้าที่เป็น Interface สำหรับ PrimeService ซึ่งจะกำหนดเมธอดที่ PrimeService ต้องมีการ implement
    public interface IPrimeService
    {
        PrimeResponse CalculatePrimeNeighbors(PrimeRequest request);
    }
}