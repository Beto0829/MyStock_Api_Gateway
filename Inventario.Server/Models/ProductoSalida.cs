using Inventario.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inventarios.Server.Models
{
    public class ProductoSalida
    {
        public int Id { get; set; }

        public int IdSalida { get; set; }

        public int IdCategoria { get; set; }

        public int IdProducto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Descuento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorDescuento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [JsonIgnore]
        public Salida? Salida { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

        [JsonIgnore]
        public Producto? Producto { get; set; }
    }
}
