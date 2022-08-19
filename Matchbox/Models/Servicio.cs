namespace Matchbox.Models
{
    public class Servicio : BaseModel
    {
        public int IdRubro { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}