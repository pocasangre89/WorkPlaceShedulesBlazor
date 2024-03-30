    using System.ComponentModel.DataAnnotations;

namespace WorkPlaceShedulesBlazor.DTO
{
    public class WorkGroupsDTO
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        [MaxLength(500)]
        public string GroupDescription { get; set; } = string.Empty;
        [Required]
        [MaxLength(250)]
        public string GroupName { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; } = DateTime.Now;
        public string Creator { get; set; } = "admin";
        public string? Modifier { get; set; } = "admin";
    }
}
