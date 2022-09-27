namespace Matchbox.ViewModels
{
    public class UserItemListViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public int? ClienteId { get; set; }

        public int? EmpresaId { get; set; }
    }
}