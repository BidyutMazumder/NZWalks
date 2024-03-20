using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.Request
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [Range(0, 50)]
        public string Code { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
