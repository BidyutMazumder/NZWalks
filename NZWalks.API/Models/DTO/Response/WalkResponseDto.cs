namespace NZWalks.API.Models.DTO.Response
{
    public class WalkResponseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        //public guid regionid { get; set; }
        //public guid difficultyid { get; set; }

        public DifficultyResponseDto Difficulty { get; set; }
        public RegionResponseDto Region { get; set; }

    }
}
