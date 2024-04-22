using Inventarios.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Inventario.Server.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Nombre { get; set; }

        [JsonIgnore]
        public List<Producto>? Productos { get; set; }

        [JsonIgnore]
        public List<Entrada>? Compras { get; set; }

        [JsonIgnore]
        public List<ProductoSalida>? ProductoSalidas { get; set; }
    }
}
