﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.Request
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be 100 character Max")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Code has to be 3 character Max")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
    }
}
