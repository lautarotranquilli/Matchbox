using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Matchbox.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Ingrese un email.")]
        [EmailAddress(ErrorMessage = "Formato de email no válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese una contraseña.")]
        [DisplayName("Contraseña")]
        public string Contrasena { get; set; }
    }
}