using Inventario.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Inventarios.Server.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(999, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Descripcion { get; set; }

        public int IdCategoria { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

        [JsonIgnore]
        public List<Entrada>? Compras { get; set; }

        [JsonIgnore]
        public List<ProductoSalida>? ProductoSalidas { get; set; }

    }
}
