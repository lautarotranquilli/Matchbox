using System;

namespace Matchbox.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}