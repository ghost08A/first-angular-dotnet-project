using back.DTOs;
using back.Repositories;

namespace back.Services
{
    public class PrimeService : IPrimeService
    {
        // เรียกใช้ IPrimeHistoryRepository จาก Repository เพื่อบันทึกประวัติการคำนวณ Prime Number
        private readonly IPrimeHistoryRepository _primeHistoryRepository;
        // รับ IPrimeHistoryRepository ผ่าน Constructor Injection
        public PrimeService(IPrimeHistoryRepository primeHistoryRepository)
        {
            _primeHistoryRepository = primeHistoryRepository;
        }

        public PrimeResponse CalculatePrimeNeighbors(PrimeRequest request)
        {
            int number = request.NumberInput;

            if(number < 2)
            {
                _primeHistoryRepository.Save(new PrimeHistory
                {
                    InputValue = number,
                    PreviousPrime = -1,
                    NextPrime = -1
                });
                return new PrimeResponse
                {
                    PreviousPrime = -1,
                    NextPrime = -1
                };
            }

            int prevPrime = FindPreviousPrime(number);
            int nextPrime = FindNextPrime(number);

            // บันทึกข้อมูลตามโมเดลใหม่
            _primeHistoryRepository.Save(new PrimeHistory
            {
                InputValue = number,
                PreviousPrime = prevPrime,
                NextPrime = nextPrime
            });

            return new PrimeResponse
            {
                PreviousPrime = prevPrime,
                NextPrime = nextPrime
            };
        }

        private bool IsPrime(int numberInput)
        {
            if (numberInput <= 1) return false;
            if (numberInput == 2) return true;
            if (numberInput % 2 == 0) return false;

            for (int i = 3; i * i <= numberInput; i += 2)
            {
                if (numberInput % i == 0) return false;
            }
            return true;
        }

        private int FindPreviousPrime(int start)
        {
            int current = start - 1;
            if (current > 2 && current % 2 == 0) current--; 
            while (current >= 2)
            {
                if (IsPrime(current)) return current;
                current -= 2;
            }
            return -1;
        }

        private int FindNextPrime(int start)
        {
            int current = start + 1;
             if (current > 2 && current % 2 == 0) current++; 
            while (true)
            {
                if (IsPrime(current)) return current;
                current += 2;
            }
        }
    }
}