using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Inventarios.Server.Migrations
{
    /// <inheritdoc />
    public partial class InventarioApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(999)", maxLength: 999, nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Salidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaFactura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    CantidadProductos = table.Column<int>(type: "int", nullable: false),
                    TotalPagarConDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPagarSinDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salidas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Entradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    PrecioCompra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExistenciaInicial = table.Column<int>(type: "int", nullable: false),
                    ExistenciaActual = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(999)", maxLength: 999, nullable: true),
                    FechaEntrada = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entradas_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Entradas_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Entradas_Proveedors_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProductoSalidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSalida = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoSalidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoSalidas_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductoSalidas_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductoSalidas_Salidas_IdSalida",
                        column: x => x.IdSalida,
                        principalTable: "Salidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Sin categoria" },
                    { 2, "Deporte" },
                    { 3, "Lacteos" },
                    { 4, "Hogar" },
                    { 5, "Bebidas Gaseosas" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Celular", "Correo", "Nombre" },
                values: new object[,]
                {
                    { 1, "3102345690", "josejuandios@gmail.com", "Juan Jose" },
                    { 2, "3114315673", "jumancitopues@hotmail.com", "Brayam" },
                    { 3, "3005554307", "kjulio1212@outlook.com", "Kevin Julio" }
                });

            migrationBuilder.InsertData(
                table: "Proveedors",
                columns: new[] { "Id", "Celular", "Correo", "Nombre" },
                values: new object[,]
                {
                    { 1, "3187779090", "postocolombia@gmail.com", "Postobon" },
                    { 2, "3145623143", "mueblesjamar@hotmail.com", "Jamar" },
                    { 3, "3005554307", "colantaleche@gmail.com", "Colanta" },
                    { 4, "3005567889", "adidascolombia@gmail.com", "Adidas" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Descripcion", "IdCategoria", "Nombre" },
                values: new object[,]
                {
                    { 1, "Leche sin conservantes de 250l", 3, "Leche deslactosada" },
                    { 2, "Perfectas para jugar tennis, duracion de hasta 2 años sino te regresamos tu dinero", 2, "Pelotas de tennis" },
                    { 3, "Yogurt sin azucar de 280ml", 3, "Yogurt griego" },
                    { 4, "La mas alta calidad exportada de japon, contiene todas las vitaminas que tu cuerpo necesita de 500ml", 3, "Leche premium" },
                    { 5, "presentacion de 175l", 5, "Gaseosa litron postobon" },
                    { 6, "Perfecto para tu sala de 5 puestos", 4, "Mueble familiar" },
                    { 7, "Presentacion en lata de 180ml", 5, "Gaseosa en lata" },
                    { 8, "Balon del FPC", 2, "Balon de futbol profesional" },
                    { 9, "Cuaderno norma de 100 hojas", 1, "Cuaderno cuadriculado 100 hojas" },
                    { 10, "Huevos 100% de campo", 1, "huevos criollos" }
                });

            migrationBuilder.InsertData(
                table: "Salidas",
                columns: new[] { "Id", "CantidadProductos", "FechaFactura", "IdCliente", "TotalDescuento", "TotalPagarConDescuento", "TotalPagarSinDescuento" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2024, 4, 22, 13, 59, 0, 0, DateTimeKind.Unspecified), 3, 26000m, 274000m, 300000m },
                    { 2, 1, new DateTime(2024, 4, 22, 13, 59, 0, 0, DateTimeKind.Unspecified), 1, 190000m, 3610000m, 3800000m },
                    { 3, 2, new DateTime(2024, 4, 22, 13, 59, 0, 0, DateTimeKind.Unspecified), 2, 7040m, 164960m, 172000m }
                });

            migrationBuilder.InsertData(
                table: "Entradas",
                columns: new[] { "Id", "ExistenciaActual", "ExistenciaInicial", "FechaEntrada", "IdCategoria", "IdProducto", "IdProveedor", "Nota", "PrecioCompra", "PrecioVenta" },
                values: new object[,]
                {
                    { 1, 30, 50, new DateTime(2024, 4, 22, 13, 59, 20, 139, DateTimeKind.Local).AddTicks(9356), 3, 3, 3, "Yogurt marca colanta sabores fresa y melocoton", 5800m, 8000m },
                    { 2, 80, 200, new DateTime(2024, 4, 22, 13, 59, 20, 139, DateTimeKind.Local).AddTicks(9395), 5, 7, 1, "La idea es venderlas en maximo un mes", 1800m, 2600m },
                    { 3, 40, 60, new DateTime(2024, 4, 22, 13, 59, 20, 139, DateTimeKind.Local).AddTicks(9417), 2, 8, 4, "Producto al cual sacarle mucho provecho por su precio de compra y de venta", 50000m, 190000m }
                });

            migrationBuilder.InsertData(
                table: "ProductoSalidas",
                columns: new[] { "Id", "Cantidad", "Descuento", "IdCategoria", "IdProducto", "IdSalida", "Precio", "Total", "ValorDescuento" },
                values: new object[,]
                {
                    { 1, 5, 0m, 3, 3, 1, 8000m, 40000m, 0m },
                    { 2, 100, 10m, 5, 7, 1, 2600m, 234000m, 26000m },
                    { 3, 20, 5m, 2, 8, 2, 190000m, 3610000m, 190000m },
                    { 4, 15, 5m, 3, 3, 3, 8000m, 114000m, 6000m },
                    { 5, 20, 2m, 5, 7, 3, 2600m, 50960m, 1040m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Nombre",
                table: "Categorias",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Celular",
                table: "Clientes",
                column: "Celular",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Correo",
                table: "Clientes",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_IdCategoria",
                table: "Entradas",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_IdProducto",
                table: "Entradas",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_IdProveedor",
                table: "Entradas",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdCategoria",
                table: "Productos",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoSalidas_IdCategoria",
                table: "ProductoSalidas",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoSalidas_IdProducto",
                table: "ProductoSalidas",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoSalidas_IdSalida",
                table: "ProductoSalidas",
                column: "IdSalida");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedors_Celular",
                table: "Proveedors",
                column: "Celular",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proveedors_Correo",
                table: "Proveedors",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_IdCliente",
                table: "Salidas",
                column: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entradas");

            migrationBuilder.DropTable(
                name: "ProductoSalidas");

            migrationBuilder.DropTable(
                name: "Proveedors");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Salidas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
