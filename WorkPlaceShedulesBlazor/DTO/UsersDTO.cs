using System.ComponentModel.DataAnnotations;

namespace WorkPlaceShedulesBlazor.DTO
{
    public class UsersDTO
    {
        public int UserId { get; set; }
        [Required(ErrorMessage ="El Codigo de usuario es obligatorio")]
        [MaxLength(50)]
        public string UserCode { get; set; } = string.Empty;
        [MaxLength(200)]
        [Required(ErrorMessage = "El Nombrel del Usuario es Obligatorio")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Correo es obligatorio")]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public bool IsAdmin { get; set; } = false;
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
