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
        [MinLength(8, ErrorMessage = "La contraseña debe contener más de 8 caracteres.")]
        [RegularExpression("^(?=.*'\'d)(?=.*[a-z])(?=.*[A-Z])(?=.*'\'W).*$", ErrorMessage = "La contraseña debe tener al menos una mayúscula, una minúscula, un número y un símbolo especial.")] 
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Repita la contraseña.")]
        [DisplayName("Repetir contraseña")]
        public string ReingresarContrasena { get; set; }
    }
}