using System;
using System.ComponentModel.DataAnnotations;

namespace NewZap_v2.Models
{
    public class PhoneData
    {
        [Key]
        public int PhoneID { get; set; }

        [Required]
        public int DDD { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }

    public class RandomNumber
    {
        [Key]
        public static int Number { get; set; }

        public int GenerateRandomNumber()
        {
            var num = new Random();
            int val = num.Next();

            return val;
        }
    }
}

