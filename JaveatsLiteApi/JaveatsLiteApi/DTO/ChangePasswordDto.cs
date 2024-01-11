using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.DTO
{
    public class ChangePasswordDto
    {
        [Required]
        public string oldPassword { get; set; }
        [Required]
        public string newPassword { get; set; }
        [Required]
        [Compare("newPassword")]
        public string cofirmNewPassword { get; set; }
    }
}
