using System.ComponentModel.DataAnnotations;

namespace Inventarios.Server.Models
{
    public class Reporte
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(-1)]
        public required string Descripcion { get; set; }
    }
}
