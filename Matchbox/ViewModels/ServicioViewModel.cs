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
        [StringLength(50, ErrorMessage = "El nombre no debe superar los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese una descripción.")]
        [StringLength(1000, ErrorMessage = "La descripción no debe superar los 1000 caracteres.")]
        public string Descripcion { get; set; }

        public string eNombre { get; set; }

        public string rNombre { get; set; }

        public Empresa empresa { get; set; }
    }
}