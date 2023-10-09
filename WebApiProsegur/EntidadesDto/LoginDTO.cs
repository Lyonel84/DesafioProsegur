using System.ComponentModel.DataAnnotations;

namespace WebApiProsegur.EntidadesDto
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Required")]
        [StringLength(15)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
