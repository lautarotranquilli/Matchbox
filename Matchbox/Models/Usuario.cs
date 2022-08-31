using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Matchbox.Models
{
    public class Usuario : BaseModel
    {
        [Required(ErrorMessage = "Ingrese un nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un apellido.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese un email.")]
        [EmailAddress(ErrorMessage = "Formato de email no válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese una contraseña.")]
        [DisplayName("Contraseña")]
        [RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener entre 8 y 16 caracteres, al menos un dígito, al menos una minúscula y al menos una mayúscula.")] 
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Repita la contraseña.")]
        [DisplayName("Repetir contraseña")]
        public string ReingresarContrasena { get; set; }
    }
}