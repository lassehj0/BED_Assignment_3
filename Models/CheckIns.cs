using System;
using Microsoft.Build.Framework;

namespace Assignment3.Models
{
	public class CheckIns
	{
        public int Id { get; set; }

        [Required]

        public int RoomNumber { get; set; }

        [Required]
        public int Adults { get; set; }

        [Required]
        public int Children { get; set; }

        [Required]
        public DateTime Date { get; set; }
        
        
    }
}
