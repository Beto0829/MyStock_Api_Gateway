using Inventario.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Inventarios.Server.Models
{
    public class Entrada
    {
        public int Id { get; set; }

        public int IdCategoria { get; set; }

        public int IdProducto { get; set; }

        public int IdProveedor { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioCompra { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioVenta { get; set; }

        public int ExistenciaInicial { get; set; }

        public int ExistenciaActual { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(999, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string? Nota { get; set; }

        public DateTime FechaEntrada { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

        [JsonIgnore]
        public Producto? Producto { get; set; }

        [JsonIgnore]
        public Proveedor? proveedor { get; set; }
    }
}
