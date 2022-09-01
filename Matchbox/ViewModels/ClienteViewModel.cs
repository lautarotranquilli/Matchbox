using Matchbox.Validations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Matchbox.Models
{
    public class ClienteViewModel : BaseModel
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un apellido.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese un teléfono.")]
        [StringLength(10, ErrorMessage = "Formato de teléfono no válido.")]
        [DisplayName("Teléfono (sin 0 ni 15)")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Ingrese un email.")]
        [EmailAddress(ErrorMessage = "Formato de email no válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese una provencia.")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Ingrese una localidad.")]
        public string Localidad { get; set; }

        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)]
        [DisplayName("Foto de perfil")]
        public IFormFile FotoPerfil { get; set; }
        public string FotoPerfilPath { get; set; }
        public string FotoPerfilPath_Old { get; set; }
    }
}