using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inventarios.Server.Models
{
    public class Salida
    {
        public int Id { get; set; }

        public DateTime FechaFactura { get; set; }

        public int IdCliente { get; set; }

        public int CantidadProductos { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPagarConDescuento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPagarSinDescuento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDescuento { get; set; }

        public List<ProductoSalida>? ProductoSalidas { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }
    }
}
