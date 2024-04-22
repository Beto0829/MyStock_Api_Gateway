using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventarios.Server.Models
{
    public class EntradaDTO
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

        [DataType(DataType.MultilineText)]
        [MaxLength(999, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string? Nota { get; set; }
    }
}
