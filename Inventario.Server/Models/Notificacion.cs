using System.ComponentModel.DataAnnotations;

namespace Inventarios.Server.Models
{
    public class Notificacion
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Titulo { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string  Cuerpo { get; set; }

        public required DateTime Fecha { get; set; }

        public bool Estado { get; set; }

        public required string Email { get; set; }
    }
}
