using Matchbox.Models;
using System.ComponentModel.DataAnnotations;

namespace Matchbox.ViewModels
{
    public class RubrosViewModel : BaseModel
    {
        [Required]
        public string Nombre { get; set; }
    }
}
