using Matchbox.Models;
using System.ComponentModel.DataAnnotations;

namespace Matchbox.ViewModels
{
    public class ServicioViewModel : BaseModel
    {
        [Required(ErrorMessage = "Seleccione un rubro.")]
        public int IdRubro { get; set; }

        [Required]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese una descripción.")]
        public string Descripcion { get; set; }
    }
}