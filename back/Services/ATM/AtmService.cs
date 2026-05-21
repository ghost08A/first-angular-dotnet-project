using back.DTOs;
using back.Repositories;

namespace back.Services
{
    public class AtmService : IAtmService
    {
      private readonly IAtmHistoryRepository _atmHistoryRepository;

        public AtmService(IAtmHistoryRepository atmHistoryRepository)
        {
            _atmHistoryRepository = atmHistoryRepository;
        }

        public AtmResponse CalculateWithdrawal(AtmRequest request)
        {
            int amount = request.Amount;
            if(amount <= 0 || amount%100 != 0)
            {
                _atmHistoryRepository.Save(new AtmHistory
            {
                InputAmount = request.Amount,
                ThousandCount = 0,
                FiveHundredCount = 0,
                OneHundredCount = 0
            });
            return new AtmResponse
            {
                Thousand = 0,
                FiveHundred = 0,
                OneHundred = 0
            };
            }
            
            int thousands = amount / 1000;
            amount %= 1000;
            int fiveHundreds = amount / 500;
            amount %= 500;
            int hundreds = amount / 100;
        
_atmHistoryRepository.Save(new AtmHistory
            {
                InputAmount = request.Amount,
                ThousandCount = thousands,
                FiveHundredCount = fiveHundreds,
                OneHundredCount = hundreds
            });
            return new AtmResponse
            {
                Thousand = thousands,
                FiveHundred = fiveHundreds,
                OneHundred = hundreds
            };
        }
    }
    }
