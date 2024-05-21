using System.ComponentModel.DataAnnotations;

namespace Inventarios.Server.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Nombre { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Direccion { get; set; }

        public required string Telefono { get; set; }

        public required string Usuario { get; set; }
    }
}
