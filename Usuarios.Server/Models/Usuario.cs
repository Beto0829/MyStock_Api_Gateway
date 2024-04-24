using System.ComponentModel.DataAnnotations;

namespace Usuarios.Server.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string UserName { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Email { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Password { get; set; }
    }
}
