using back.DTOs;

namespace back.Services
{
    public interface IAtmService
    {
        AtmResponse CalculateWithdrawal(AtmRequest request);
    }
}