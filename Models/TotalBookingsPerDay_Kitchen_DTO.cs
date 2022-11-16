using Microsoft.Build.Framework;

namespace Assignment3.Models
{
	public class TotalBookingsPerDay_Kitchen_DTO
	{

		

        [Required]
        public int TotalAdults { get; set; }

        [Required]
        public int TotalChildren { get; set; }

        [Required] 
        public int TotalGuests { get; set; }

        [Required]
        public int CheckedInAdults { get; set; }

        [Required]
        public int CheckedInChildren { get; set; }

        [Required]
        public int RemainingAdults { get; set; }

        [Required]
        public int RemainingChildren { get; set; }

        [Required]
        public int RemainingGuests { get; set; }

    }
}
