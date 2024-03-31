using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkPlaceShedulesBlazor.DTO
{
    public class UserWorkPlaceShedulesDTO
    {
        [Key]
        public int UserWorkPlaceScheduleId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Schedule { get; set; } = DateTime.Now;
        [Required] 
        public bool IsAdminRequest { get; set; } = false;
        [Required]
        public bool IsActive { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; } = DateTime.Now;
        public string Creator { get; set; } = "admin";
        public string Modifier { get; set; } = "admin";

    }
}
