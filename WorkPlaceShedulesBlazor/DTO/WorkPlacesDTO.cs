using System.ComponentModel.DataAnnotations;

namespace WorkPlaceShedulesBlazor.DTO
{
    public class WorkPlacesDTO
    {
        [Key]
        public int WorkPlaceId { get; set; }
        [Required]
        [MaxLength(50)]
        public string WorkPlaceCode { get; set; } = string.Empty;
        [Required]
        [MaxLength(500)]
        public string WorkPlaceName { get; set; } = string.Empty;
        [Required]
        public int WorkPlaceNumber { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; } = DateTime.Now;
        public string Creator { get; set; } = "admin";
        public string? Modifier { get; set; } = "admin";
    }
}
