using System;

namespace Matchbox.ViewModels
{
    public class ServiciosListViewModel
    {
        public int sId { get; set; }
        public string sNombre { get; set; }
        public string rNombre { get; set; }
        public DateTime? sFechaBaja { get; set; }
        public int sEmpresa { get; set; }
    }
}