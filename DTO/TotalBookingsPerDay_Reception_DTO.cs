using Microsoft.Build.Framework;

namespace Assignment3.DTO
{
    public class TotalBookingsPerDay_Reception_DTO
    {



        [Required]
        public int TotalAdults { get; set; }

        [Required]
        public int TotalChildren { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}
