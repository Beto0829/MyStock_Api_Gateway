using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Inventarios.Server.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Nombre { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Celular { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Correo { get; set; }

        [JsonIgnore]
        public List<Salida>? Salidas { get; set; }
    }
}
