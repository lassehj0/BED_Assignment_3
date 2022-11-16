using Microsoft.Build.Framework;

namespace Assignment3.Models
{
	public class TotalBookingsPerDay
	{

		public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int TotalAdults { get; set; }

        [Required]
        public int TotalChildren { get; set; }

       
    }
}
