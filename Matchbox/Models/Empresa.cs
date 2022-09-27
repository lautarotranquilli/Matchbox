namespace Matchbox.Models
{
    public class Empresa : BaseModel
    {
        public int IdUsuario { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string IdProvincia { get; set; }
        public string IdLocalidad { get; set; }
        public string ProfilePath { get; set; }
    }
}