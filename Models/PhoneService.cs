using System;
using System.Linq;

namespace NewZap_v2.Models
{
    public static class PhoneService
    {
        public static bool IsExisting(this PhoneData payload, PhoneContext IdContext)
        {
            var validatedNumber = IdContext.Phones.FirstOrDefault(x => x.DDD == payload.DDD && x.PhoneNumber == payload.PhoneNumber);
            if (validatedNumber != null)
            {
                return true;
            }
            return false;
        }

        public static bool SaveNumber(this PhoneData payload, PhoneContext IdContext)
        {
            IdContext.Phones.Add(payload);
            return true;
        }

        public static string GenerateRandomNumber()
        {
            var num = new Random();
            int val = num.Next();

            return val.ToString();
        }
    }
}
