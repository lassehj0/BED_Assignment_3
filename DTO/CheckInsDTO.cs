using System;
using Microsoft.Build.Framework;

namespace Assignment3.DTO
{
    public class CheckInsDTO
    {
        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public int Adults { get; set; }

        [Required]
        public int Children { get; set; }
    }
}
