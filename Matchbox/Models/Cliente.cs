namespace Matchbox.Models
{
    public class Cliente : BaseModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string IdProvincia { get; set; }
        public string IdLocalidad { get; set; }
        public string ProfilePath { get; set; }
    }
}