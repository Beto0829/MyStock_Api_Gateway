using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Inventarios.Server.Migrations
{
    /// <inheritdoc />
    public partial class APIINVENTARY : Migration
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
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cuerpo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
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
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    IdEntrada = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_ProductoSalidas_Entradas_IdEntrada",
                        column: x => x.IdEntrada,
                        principalTable: "Entradas",
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
                    { 5, "Bebidas Gaseosas" },
                    { 6, "Frutas" },
                    { 7, "Verduras" },
                    { 8, "Carnes" },
                    { 9, "Abarrotes" },
                    { 10, "Congelados" },
                    { 11, "Panadería" },
                    { 12, "Dulces y chocolates" },
                    { 13, "Bebidas alcohólicas" },
                    { 14, "Bebidas no alcohólicas" },
                    { 15, "Cuidado personal" },
                    { 16, "Electrónica" },
                    { 17, "Ropa" },
                    { 18, "Libros y revistas" },
                    { 19, "Juguetes" },
                    { 20, "Herramientas" },
                    { 21, "Decoración" },
                    { 22, "Automóviles" },
                    { 23, "Jardinería" },
                    { 24, "Mascotas" },
                    { 25, "Instrumentos musicales" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Celular", "Correo", "Nombre" },
                values: new object[,]
                {
                    { 1, "3102345690", "josejuandios@gmail.com", "Juan Jose" },
                    { 2, "3114315673", "jumancitopues@hotmail.com", "Brayam" },
                    { 3, "3005554307", "kjulio1212@outlook.com", "Kevin Julio" },
                    { 4, "3152348765", "mferna@gmail.com", "María Fernanda" },
                    { 5, "3178765432", "pedroperez@hotmail.com", "Pedro Pérez" },
                    { 6, "3129876543", "lauramartinez@gmail.com", "Laura Martínez" },
                    { 7, "3101234567", "sofiagomez@hotmail.com", "Sofía Gómez" },
                    { 8, "3009876543", "carlosrodriguez@gmail.com", "Carlos Rodríguez" },
                    { 9, "3187654321", "anamaria@hotmail.com", "Ana María" },
                    { 10, "3145678901", "diegosanchez@gmail.com", "Diego Sánchez" },
                    { 11, "3112345678", "camilalopez@hotmail.com", "Camila López" },
                    { 12, "3198765432", "andresperez@gmail.com", "Andrés Pérez" },
                    { 13, "3167890123", "julianatorres@hotmail.com", "Juliana Torres" },
                    { 14, "3136789012", "joseluis@gmail.com", "José Luis" },
                    { 15, "3108901234", "danielaramirez@hotmail.com", "Daniela Ramírez" },
                    { 16, "3174567890", "gabrielgutierrez@gmail.com", "Gabriel Gutiérrez" },
                    { 17, "3006789012", "valentinadiaz@hotmail.com", "Valentina Díaz" },
                    { 18, "3190123456", "davidgarcia@gmail.com", "David García" },
                    { 19, "3124567890", "marianasilva@hotmail.com", "Mariana Silva" },
                    { 20, "3147890123", "alejandrovargas@gmail.com", "Alejandro Vargas" }
                });

            migrationBuilder.InsertData(
                table: "Proveedors",
                columns: new[] { "Id", "Celular", "Correo", "Nombre" },
                values: new object[,]
                {
                    { 1, "3187779090", "adidascolombia@gmail.com", "Adidas" },
                    { 2, "3145623143", "colontacol@hotmail.com", "Colanta" },
                    { 3, "3005554307", "mueblesjamar@gmail.com", "Jamar" },
                    { 4, "3005567889", "postoboncol@gmail.com", "Postobon" },
                    { 5, "3156789012", "frutastropicales@hotmail.com", "Frutas Tropicales" },
                    { 6, "3159876543", "Donve@gmail.com", "Verduras Don Colombia" },
                    { 7, "3128901234", "donjuancarniceria@gmail.com", "Carnicería Don Juan" },
                    { 8, "3188765432", "abarro@hotmail.com", "Super Abarrotes La Familia" },
                    { 9, "3112345678", "elvene12345@hotmail.com", "Congelados el vene" },
                    { 10, "3198765432", "bimbocolombia@gmail.com", "Bimbo" },
                    { 11, "3105678901", "colombina@hotmail.com", "Colombina" },
                    { 12, "3146789012", "clubCo@gmail.com", "Club Colombia" },
                    { 13, "3177890123", "Brisa@hotmail.com", "Brisa" },
                    { 14, "3128901200", "gille@gmail.com", "Gillette" },
                    { 15, "3189012345", "IntelCo@gmail.com", "Intel Co." },
                    { 16, "3150123456", "NikeCol@hotmail.com", "Nike" },
                    { 17, "3191234567", "Elrincon@gmail.com", "Rincon del vago" },
                    { 18, "3102345678", "MatelsaJuguetes@gmail.com", "Matelsa" },
                    { 19, "3153456789", "IngcoCO@hotmail.com", "IngCo" },
                    { 20, "3124567890", "brissaa@gmail.com", "Brissa" },
                    { 21, "3175678901", "NissanCOl@hotmail.com", "Nissan" },
                    { 22, "3146789100", "Tie@gmail.com", "TierraGro" },
                    { 23, "3197890123", "RingoCo@hotmail.com", "Ringo" },
                    { 24, "3108901234", "Musicalyamaha@gmail.com", "Yamaha Musical" }
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
                    { 10, "Huevos 100% de campo", 1, "huevos criollos" },
                    { 11, "Manzanas frescas y jugosas de variedad verde", 6, "Manzanas verdes" },
                    { 12, "Piñas maduras y dulces de origen tropical", 6, "Piñas tropicales" },
                    { 13, "Naranjas valencianas de alta calidad y sabor intenso", 6, "Naranjas Valencia" },
                    { 14, "Plátanos maduros de la variedad dominicana", 6, "Plátanos dominicos" },
                    { 15, "Tomate fresco de la mejor calidad", 7, "Tomate" },
                    { 16, "Lechuga fresca y crujiente", 7, "Lechuga" },
                    { 17, "Pepino verde y fresco", 7, "Pepino" },
                    { 18, "Filete de res de alta calidad", 8, "Filete de Res" },
                    { 19, "Pechuga de pollo fresca y jugosa", 8, "Pechuga de Pollo" },
                    { 20, "Chuleta de cerdo fresca y tierna", 8, "Chuleta de Cerdo" },
                    { 21, "Arroz blanco de grano largo", 9, "Arroz" },
                    { 22, "Filete de salmón congelado al vacío", 10, "Filete de salmón congelado" },
                    { 23, "Pizza congelada de pepperoni lista para hornear", 10, "Pizza congelada de pepperoni" },
                    { 24, "Helado de vainilla en envase de 1 litro", 10, "Helado de vainilla" },
                    { 25, "Croquetas de pollo congeladas, caja de 500g", 10, "Croquetas de pollo" },
                    { 26, "Papas fritas congeladas, bolsa de 1kg", 10, "Papas fritas congeladas" },
                    { 27, "Mezcla de vegetales congelados, bolsa de 500g", 10, "Vegetales mixtos congelados" },
                    { 28, "Pan blanco fresco, barra de 500g", 11, "Pan blanco" },
                    { 29, "Croissant de mantequilla recién horneado", 11, "Croissant de mantequilla" },
                    { 30, "Tableta de chocolate con leche", 12, "Chocolate con leche" },
                    { 31, "Tableta de chocolate amargo, 70% cacao", 12, "Chocolate amargo" },
                    { 32, "Bolsa de gomitas de frutas surtidas", 12, "Gomitas de frutas" },
                    { 33, "Bolsa de malvaviscos cubiertos de azúcar", 12, "Malvaviscos" },
                    { 34, "Bolsa de caramelos surtidos", 12, "Caramelos surtidos" },
                    { 35, "Paquete de chicles de menta, sin azúcar", 12, "Chicle de menta" },
                    { 36, "Caja de bombones de chocolate surtidos", 12, "Bombones de chocolate" },
                    { 37, "Paquete de alfajores de dulce de leche, cubiertos de chocolate", 12, "Alfajores de dulce de leche" },
                    { 38, "Botella de agua mineral sin gas", 14, "Agua mineral" },
                    { 39, "Botella de refresco de cola de 2 litros", 14, "Refresco de cola" },
                    { 40, "Envase de jugo de naranja natural de 1 litro", 14, "Jugo de naranja" },
                    { 41, "Botella de vino tinto reserva de 750 ml", 13, "Vino tinto reserva" },
                    { 42, "Six-pack de cerveza artesanal India Pale Ale (IPA)", 13, "Cerveza artesanal IPA" },
                    { 43, "Shampoo para fortalecer el cabello, envase de 400 ml", 15, "Shampoo fortificante" },
                    { 44, "Crema facial hidratante con ácido hialurónico, envase de 50 ml", 15, "Crema hidratante facial" },
                    { 45, "Desodorante en barra para hombre, sin alcohol, envase de 50 g", 15, "Desodorante en barra" },
                    { 46, "Hilo dental con menta, 50 metros", 15, "Dental floss" },
                    { 47, "Auriculares Bluetooth con cancelación de ruido, color negro", 16, "Auriculares inalámbricos" },
                    { 48, "Reloj inteligente resistente al agua con GPS integrado, color rojo", 16, "Smartwatch deportivo" },
                    { 49, "Base de carga inalámbrica rápida para teléfonos compatibles, color blanco", 16, "Cargador inalámbrico" },
                    { 50, "Power bank de 20000 mAh con puerto USB-C y USB-A, color gris", 16, "Batería externa" },
                    { 51, "Altavoz portátil con sonido estéreo de alta definición, color azul", 16, "Altavoz Bluetooth" },
                    { 52, "Cámara IP para interiores con visión nocturna y detección de movimiento, color blanco", 16, "Cámara de seguridad Wi-Fi" },
                    { 53, "Teclado retroiluminado con switches mecánicos y anti-ghosting, color negro", 16, "Teclado mecánico gaming" },
                    { 54, "Mouse ergonómico con sensor óptico de alta precisión, color gris", 16, "Mouse óptico" },
                    { 55, "Router inalámbrico de última generación con velocidad de hasta 6000 Mbps, color negro", 16, "Router Wi-Fi 6" },
                    { 56, "Tableta digitalizadora con lápiz sensible a la presión, ideal para diseño gráfico, color blanco", 16, "Tableta gráfica" },
                    { 57, "Bombilla LED inteligente controlable por voz y aplicación móvil, color blanco cálido", 16, "Lámpara inteligente" },
                    { 58, "Camisa de algodón con cuello clásico y botones frontales, color blanco", 17, "Camisa de vestir" },
                    { 59, "Jeans de corte recto y diseño clásico, color azul índigo", 17, "Pantalones vaqueros" },
                    { 60, "Vestido largo de satén con escote en V y detalle de encaje, color negro", 17, "Vestido de fiesta" },
                    { 61, "Tenis para correr con amortiguación y suela de goma, color gris y rosa", 17, "Zapatillas deportivas" },
                    { 62, "Bufanda suave y abrigada tejida a mano, color gris jaspeado", 17, "Bufanda de lana" },
                    { 63, "Colección de recetas auténticas de diversas regiones del mundo", 18, "Libro de cocina: Recetas tradicionales" },
                    { 64, "Edición mensual con los estilos más actuales y consejos de moda", 18, "Revista de moda: Últimas tendencias" },
                    { 65, "Muñeca de 30 cm con ropa intercambiable y accesorios", 19, "Muñeca articulada" },
                    { 66, "Set de carreras con pistas, coches y accesorios para montar", 19, "Circuito de carreras" },
                    { 67, "Rompecabezas de paisajes naturales para armar en familia", 19, "Rompecabezas de 1000 piezas" },
                    { 68, "Set de 200 bloques de colores para construir y crear", 19, "Set de bloques de construcción" },
                    { 69, "Peluche suave de 1 metro de altura para abrazar", 19, "Peluche gigante de oso" },
                    { 70, "Set con materiales para crear manualidades y proyectos artísticos", 19, "Kit de manualidades para niños" },
                    { 71, "Juego de mesa interactivo para explorar la selva y superar desafíos", 19, "Juego de mesa: Aventuras en la selva" },
                    { 72, "Set de 6 destornilladores de diferentes tamaños y puntas", 20, "Juego de destornilladores" },
                    { 73, "Taladro con batería recargable, ideal para trabajos domésticos", 20, "Taladro eléctrico inalámbrico" },
                    { 74, "Caja resistente con compartimentos y ruedas para transportar herramientas", 20, "Caja de herramientas con ruedas" },
                    { 75, "Cuadro decorativo con diseño abstracto y marco de madera", 21, "Cuadro abstracto moderno" },
                    { 76, "Set de 4 cojines con diferentes diseños y colores para sofá o cama", 21, "Cojines decorativos" },
                    { 77, "Jarrón decorativo de cerámica con diseño elegante para flores", 21, "Jarrón de cerámica" },
                    { 78, "Espejo de pared con marco de metal negro, ideal para entrada o sala", 21, "Espejo de pared rectangular" },
                    { 79, "Llantas de alta performance para vehículos deportivos", 22, "Llantas deportivas" },
                    { 80, "Kit completo de luces LED para mejorar la iluminación del vehículo", 22, "Kit de luces LED para automóviles" },
                    { 81, "Podadora eléctrica para cortar el césped de jardines pequeños y medianos", 23, "Podadora de césped eléctrica" },
                    { 82, "Collar resistente y ajustable para perros de todos los tamaños", 24, "Collar ajustable para perros" },
                    { 83, "Juguete interactivo para gatos con plumas y sonidos que estimulan su instinto de caza", 24, "Juguete para gatos con plumas" },
                    { 84, "Comedero automático programable para perros y gatos que dispensa comida en horarios predefinidos", 24, "Comedero automático para mascotas" },
                    { 85, "Guitarra acústica de calidad con cuerdas de nylon para principiantes", 25, "Guitarra acústica" },
                    { 86, "Teclado electrónico de 61 teclas con múltiples funciones y sonidos", 25, "Teclado electrónico" },
                    { 87, "Batería infantil de tamaño compacto con todo lo necesario para empezar a tocar", 25, "Batería completa para niños" },
                    { 88, "Violín de alta calidad hecho a mano con madera de abeto y arce", 25, "Violín profesional" },
                    { 89, "Micrófono de condensador con soporte y filtro anti-pop para grabaciones profesionales", 25, "Micrófono de condensador" },
                    { 90, "Piano digital portátil con teclado contrapesado y múltiples sonidos de alta calidad", 25, "Piano digital de escenario" }
                });

            migrationBuilder.InsertData(
                table: "Salidas",
                columns: new[] { "Id", "CantidadProductos", "FechaFactura", "IdCliente", "TotalDescuento", "TotalPagarConDescuento", "TotalPagarSinDescuento" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2024, 11, 20, 7, 4, 0, 0, DateTimeKind.Unspecified), 3, 26000m, 274000m, 300000m },
                    { 2, 1, new DateTime(2024, 11, 20, 7, 4, 0, 0, DateTimeKind.Unspecified), 1, 190000m, 3610000m, 3800000m },
                    { 3, 2, new DateTime(2024, 11, 20, 7, 4, 0, 0, DateTimeKind.Unspecified), 2, 7040m, 164960m, 172000m }
                });

            migrationBuilder.InsertData(
                table: "Entradas",
                columns: new[] { "Id", "Estado", "ExistenciaActual", "ExistenciaInicial", "FechaEntrada", "IdCategoria", "IdProducto", "IdProveedor", "Nota", "PrecioCompra", "PrecioVenta" },
                values: new object[,]
                {
                    { 1, "Activo", 50, 50, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 11, 5, "", 20000m, 80000m },
                    { 2, "Activo", 150, 150, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 20, 7, "", 90000m, 200000m },
                    { 3, "Activo", 50, 50, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 23, 9, "", 10000m, 35000m },
                    { 4, "Activo", 200, 200, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 26, 9, "", 4000000m, 4700000m },
                    { 5, "Activo", 50, 50, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 29, 10, "", 35000m, 80000m },
                    { 6, "Activo", 400, 400, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 31, 11, "", 58000m, 80000m },
                    { 7, "Activo", 100, 100, new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 34, 11, "", 45000m, 80000m },
                    { 8, "Activo", 300, 300, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 35, 11, "", 65000m, 215680m },
                    { 9, "Activo", 70, 70, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 42, 12, "", 60999m, 120000m },
                    { 10, "Activo", 90, 90, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 40, 13, "", 700000m, 8000000m },
                    { 11, "Activo", 200, 200, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 45, 14, "", 5000000m, 8000000m },
                    { 12, "Activo", 135, 135, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 44, 14, "", 30000m, 40000m },
                    { 13, "Activo", 80, 80, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 47, 15, "", 400000m, 535000m },
                    { 14, "Activo", 100, 100, new DateTime(2024, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 48, 15, "", 32400m, 53490m },
                    { 15, "Activo", 50, 50, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 49, 15, "", 35690m, 109000m },
                    { 16, "Activo", 35, 35, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 50, 15, "", 22000m, 40000m },
                    { 17, "Activo", 300, 300, new DateTime(2024, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 53, 15, "", 58000m, 98000m },
                    { 18, "Activo", 50, 50, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 55, 15, "", 13000m, 80000m },
                    { 19, "Activo", 50, 50, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, 59, 16, "", 11000m, 18000m },
                    { 20, "Activo", 40, 40, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, 61, 16, "", 42300m, 67800m },
                    { 21, "Activo", 38, 38, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, 63, 17, "", 2000000m, 3500000m },
                    { 22, "Activo", 200, 200, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, 66, 18, "", 1200000m, 2000000m },
                    { 23, "Activo", 50, 50, new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, 67, 18, "", 500000m, 800000m },
                    { 24, "Activo", 70, 70, new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 73, 19, "", 250000m, 800000m },
                    { 25, "Activo", 200, 200, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, 75, 20, "", 1200000m, 2000000m },
                    { 26, "Activo", 50, 50, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, 77, 20, "", 500000m, 800000m },
                    { 27, "Activo", 70, 70, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, 78, 20, "", 250000m, 800000m },
                    { 28, "Activo", 200, 200, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, 80, 21, "", 1200000m, 2000000m },
                    { 29, "Activo", 50, 50, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, 81, 22, "", 500000m, 800000m },
                    { 30, "Activo", 70, 70, new DateTime(2024, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 84, 23, "", 250000m, 800000m },
                    { 31, "Activo", 50, 50, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 87, 24, "", 500000m, 800000m },
                    { 32, "Activo", 70, 70, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 88, 24, "", 250000m, 800000m },
                    { 33, "Activo", 200, 200, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 89, 24, "", 1200000m, 2000000m },
                    { 34, "Activo", 50, 50, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 85, 24, "", 500000m, 800000m },
                    { 35, "Activo", 70, 70, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, 4, "", 250000m, 800000m }
                });

            migrationBuilder.InsertData(
                table: "ProductoSalidas",
                columns: new[] { "Id", "Cantidad", "Descuento", "IdCategoria", "IdEntrada", "IdProducto", "IdSalida", "Precio", "Total", "ValorDescuento" },
                values: new object[,]
                {
                    { 1, 5, 0m, 6, 1, 11, 1, 8000m, 40000m, 0m },
                    { 2, 100, 10m, 8, 2, 20, 1, 200000m, 234000m, 26000m },
                    { 3, 20, 5m, 2, 3, 8, 2, 190000m, 3610000m, 190000m },
                    { 4, 15, 5m, 3, 1, 3, 3, 8000m, 114000m, 6000m },
                    { 5, 20, 2m, 5, 2, 7, 3, 2600m, 50960m, 1040m }
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
                name: "IX_Empresas_Usuario",
                table: "Empresas",
                column: "Usuario",
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
                name: "IX_ProductoSalidas_IdEntrada",
                table: "ProductoSalidas",
                column: "IdEntrada");

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
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "ProductoSalidas");

            migrationBuilder.DropTable(
                name: "Entradas");

            migrationBuilder.DropTable(
                name: "Salidas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedors");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
