using Inventario.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Server.Models
{
    public class MiDbContext : DbContext
    {
        public MiDbContext(DbContextOptions<MiDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proveedor> Proveedors { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<ProductoSalida> ProductoSalidas { get; set; }
        public DbSet<Salida> Salidas { get; set; }
        //public DbSet<Reporte> Reportes { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)            // Un producto tiene una categoría
                .WithMany(c => c.Productos)          // Una categoría puede tener varios productos
                .HasForeignKey(p => p.IdCategoria)   // La clave foránea en productos apunta a IdCategoria
                .IsRequired();

            modelBuilder.Entity<Cliente>()
               .HasIndex(c => c.Correo)
               .IsUnique();

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Celular)
                .IsUnique();

            modelBuilder.Entity<Proveedor>()
               .HasIndex(p => p.Correo)
               .IsUnique();

            modelBuilder.Entity<Proveedor>()
                .HasIndex(p => p.Celular)
                .IsUnique();

            modelBuilder.Entity<Categoria>()
               .HasIndex(c => c.Nombre)
               .IsUnique();

            modelBuilder.Entity<Empresa>()
                .HasIndex(e => e.Usuario)
                .IsUnique();

            modelBuilder.Entity<Notificacion>()
               .HasIndex(e => e.Email)
               .IsUnique();

            //relaciones del modelo entradas, aca abajo

            modelBuilder.Entity<Entrada>()
                .HasOne(e => e.Categoria)            // Una compra tiene una categoría
                .WithMany(cat => cat.Compras)        // Una categoría puede tener varias compras
                .HasForeignKey(e => e.IdCategoria)   // La clave foránea en compras apunta a IdCategoria
                .IsRequired();

            modelBuilder.Entity<Entrada>()
               .HasOne(e => e.Producto)            // Una compra tiene un producto
               .WithMany(p => p.Compras)           // Un producto puede tener varias compras
               .HasForeignKey(e => e.IdProducto)   // La clave foránea en compras apunta a IdProducto
               .IsRequired();

            modelBuilder.Entity<Entrada>()
              .HasOne(e => e.Proveedor)            // Una compra tiene un proveedor
              .WithMany(p => p.Compras)            // Un proveedor puede tener varias compras
              .HasForeignKey(e => e.IdProveedor)   // La clave foránea en compras apunta a IdProveedor
              .IsRequired();

            //Relaciones salidas

            modelBuilder.Entity<ProductoSalida>()
              .HasOne(ps => ps.Salida)               // Un Producto salida tiene una salida
              .WithMany(s => s.ProductoSalidas)     // Una salida puede tener varios producto salida
              .HasForeignKey(ps => ps.IdSalida)    // La clave foránea en compras apunta a IdSalida
              .IsRequired();

            modelBuilder.Entity<Salida>()
                .HasOne(s => s.Cliente)               // Un producto salida tiene una entrada
                .WithMany(c => c.Salidas)            // No hay navegación inversa en Entrada
                .HasForeignKey(ps => ps.IdCliente)  // La clave foránea en compras apunta a IdEntrada
                .IsRequired();

            modelBuilder.Entity<ProductoSalida>()
               .HasOne(p => p.Categoria)
               .WithMany(c => c.ProductoSalidas)
               .HasForeignKey(p => p.IdCategoria)
               .IsRequired();

            modelBuilder.Entity<ProductoSalida>()
              .HasOne(ps => ps.Producto)
              .WithMany(p => p.ProductoSalidas)
              .HasForeignKey(ps => ps.IdProducto)
              .IsRequired();

            modelBuilder.Entity<ProductoSalida>()
             .HasOne(ps => ps.Entrada)
             .WithMany(e => e.ProductoSalidas)
             .HasForeignKey(ps => ps.IdEntrada)
             .IsRequired();



            //Insertar datos cateoria
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 1, Nombre = "Sin categoria" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 2, Nombre = "Deporte" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 3, Nombre = "Lacteos" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 4, Nombre = "Hogar" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 5, Nombre = "Bebidas Gaseosas" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 6, Nombre = "Frutas" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 7, Nombre = "Verduras" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 8, Nombre = "Carnes" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 9, Nombre = "Abarrotes" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 10, Nombre = "Congelados" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 11, Nombre = "Panadería" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 12, Nombre = "Dulces y chocolates" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 13, Nombre = "Bebidas alcohólicas" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 14, Nombre = "Bebidas no alcohólicas" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 15, Nombre = "Cuidado personal" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 16, Nombre = "Electrónica" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 17, Nombre = "Ropa" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 18, Nombre = "Libros y revistas" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 19, Nombre = "Juguetes" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 20, Nombre = "Herramientas" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 21, Nombre = "Decoración" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 22, Nombre = "Automóviles" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 23, Nombre = "Jardinería" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 24, Nombre = "Mascotas" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 25, Nombre = "Instrumentos musicales" });


            //Insertar datos cliente
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 1, Nombre = "Juan Jose", Celular = "3102345690", Correo = "josejuandios@gmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 2, Nombre = "Brayam", Celular = "3114315673", Correo = "jumancitopues@hotmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 3, Nombre = "Kevin Julio", Celular = "3005554307", Correo = "kjulio1212@outlook.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 4, Nombre = "María Fernanda", Celular = "3152348765", Correo = "mferna@gmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 5, Nombre = "Pedro Pérez", Celular = "3178765432", Correo = "pedroperez@hotmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 6, Nombre = "Laura Martínez", Celular = "3129876543", Correo = "lauramartinez@gmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 7, Nombre = "Sofía Gómez", Celular = "3101234567", Correo = "sofiagomez@hotmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 8, Nombre = "Carlos Rodríguez", Celular = "3009876543", Correo = "carlosrodriguez@gmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 9, Nombre = "Ana María", Celular = "3187654321", Correo = "anamaria@hotmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 10, Nombre = "Diego Sánchez", Celular = "3145678901", Correo = "diegosanchez@gmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 11, Nombre = "Camila López", Celular = "3112345678", Correo = "camilalopez@hotmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 12, Nombre = "Andrés Pérez", Celular = "3198765432", Correo = "andresperez@gmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 13, Nombre = "Juliana Torres", Celular = "3167890123", Correo = "julianatorres@hotmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 14, Nombre = "José Luis", Celular = "3136789012", Correo = "joseluis@gmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 15, Nombre = "Daniela Ramírez", Celular = "3108901234", Correo = "danielaramirez@hotmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 16, Nombre = "Gabriel Gutiérrez", Celular = "3174567890", Correo = "gabrielgutierrez@gmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 17, Nombre = "Valentina Díaz", Celular = "3006789012", Correo = "valentinadiaz@hotmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 18, Nombre = "David García", Celular = "3190123456", Correo = "davidgarcia@gmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 19, Nombre = "Mariana Silva", Celular = "3124567890", Correo = "marianasilva@hotmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 20, Nombre = "Alejandro Vargas", Celular = "3147890123", Correo = "alejandrovargas@gmail.com" });


            //Insertar datos proveedors
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 1, Nombre = "Adidas", Celular = "3187779090", Correo = "adidascolombia@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 2, Nombre = "Colanta", Celular = "3145623143", Correo = "colontacol@hotmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 3, Nombre = "Jamar", Celular = "3005554307", Correo = "mueblesjamar@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 4, Nombre = "Postobon", Celular = "3005567889", Correo = "postoboncol@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 5, Nombre = "Frutas Tropicales", Celular = "3156789012", Correo = "frutastropicales@hotmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 6, Nombre = "Verduras Don Colombia", Celular = "3159876543", Correo = "Donve@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 7, Nombre = "Carnicería Don Juan", Celular = "3128901234", Correo = "donjuancarniceria@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 8, Nombre = "Super Abarrotes La Familia", Celular = "3188765432", Correo = "abarro@hotmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 9, Nombre = "Congelados el vene", Celular = "3112345678", Correo = "elvene12345@hotmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 10, Nombre = "Bimbo", Celular = "3198765432", Correo = "bimbocolombia@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 11, Nombre = "Colombina", Celular = "3105678901", Correo = "colombina@hotmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 12, Nombre = "Club Colombia", Celular = "3146789012", Correo = "clubCo@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 13, Nombre = "Brisa", Celular = "3177890123", Correo = "Brisa@hotmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 14, Nombre = "Gillette", Celular = "3128901200", Correo = "gille@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 15, Nombre = "Intel Co.", Celular = "3189012345", Correo = "IntelCo@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 16, Nombre = "Nike", Celular = "3150123456", Correo = "NikeCol@hotmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 17, Nombre = "Rincon del vago", Celular = "3191234567", Correo = "Elrincon@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 18, Nombre = "Matelsa", Celular = "3102345678", Correo = "MatelsaJuguetes@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 19, Nombre = "IngCo", Celular = "3153456789", Correo = "IngcoCO@hotmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 20, Nombre = "Brissa", Celular = "3124567890", Correo = "brissaa@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 21, Nombre = "Nissan", Celular = "3175678901", Correo = "NissanCOl@hotmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 22, Nombre = "TierraGro", Celular = "3146789100", Correo = "Tie@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 23, Nombre = "Ringo", Celular = "3197890123", Correo = "RingoCo@hotmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 24, Nombre = "Yamaha Musical", Celular = "3108901234", Correo = "Musicalyamaha@gmail.com" });
            //modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 25, Nombre = "Fanta", Celular = "3159012345", Correo = "fantacolombia@hotmail.com" });
            //modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 26, Nombre = "Ferretería La Llave", Celular = "3180123456", Correo = "lallaveferreteria@gmail.com" });
            //modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 27, Nombre = "Under Armour", Celular = "3121234567", Correo = "underarmourcolombia@hotmail.com" });
            //modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 28, Nombre = "Colombina", Celular = "3172345678", Correo = "colombinaproductos@gmail.com" });
            //modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 29, Nombre = "Ferretería El Pico", Celular = "3143456789", Correo = "elpicoferreteria@hotmail.com" });
            //modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 30, Nombre = "Energizer", Celular = "3194567890", Correo = "energizercolombia@gmail.com" });
            //modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 31, Nombre = "Carnes El Ranchero", Celular = "3105678901", Correo = "elrancherocarnes@gmail.com" });
            //modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 32, Nombre = "Frutas Tropicales", Celular = "3156789012", Correo = "frutastropicales@hotmail.com" });
            //modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 33, Nombre = "Automóviles La Ruta", Celular = "3187890123", Correo = "larutaautomoviles@gmail.com" });
            //modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 34, Nombre = "Carnicería Don Juan", Celular = "3128901234", Correo = "donjuancarniceria@gmail.com" });
            //modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 35, Nombre = "Frutas del Campo", Celular = "3199012345", Correo = "delcampofrutas@hotmail.com" });

            //Insertar datos producto
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 1, Nombre = "Leche deslactosada", Descripcion = "Leche sin conservantes de 250l", IdCategoria = 3 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 2, Nombre = "Pelotas de tennis", Descripcion = "Perfectas para jugar tennis, duracion de hasta 2 años sino te regresamos tu dinero", IdCategoria = 2 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 3, Nombre = "Yogurt griego", Descripcion = "Yogurt sin azucar de 280ml", IdCategoria = 3 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 4, Nombre = "Leche premium", Descripcion = "La mas alta calidad exportada de japon, contiene todas las vitaminas que tu cuerpo necesita de 500ml", IdCategoria = 3 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 5, Nombre = "Gaseosa litron postobon", Descripcion = "presentacion de 175l", IdCategoria = 5 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 6, Nombre = "Mueble familiar", Descripcion = "Perfecto para tu sala de 5 puestos", IdCategoria = 4 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 7, Nombre = "Gaseosa en lata", Descripcion = "Presentacion en lata de 180ml", IdCategoria = 5 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 8, Nombre = "Balon de futbol profesional", Descripcion = "Balon del FPC", IdCategoria = 2 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 9, Nombre = "Cuaderno cuadriculado 100 hojas", Descripcion = "Cuaderno norma de 100 hojas", IdCategoria = 1 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 10, Nombre = "huevos criollos", Descripcion = "Huevos 100% de campo", IdCategoria = 1 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 11, Nombre = "Manzanas verdes", Descripcion = "Manzanas frescas y jugosas de variedad verde", IdCategoria = 6 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 12, Nombre = "Piñas tropicales", Descripcion = "Piñas maduras y dulces de origen tropical", IdCategoria = 6 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 13, Nombre = "Naranjas Valencia", Descripcion = "Naranjas valencianas de alta calidad y sabor intenso", IdCategoria = 6 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 14, Nombre = "Plátanos dominicos", Descripcion = "Plátanos maduros de la variedad dominicana", IdCategoria = 6 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 15, Nombre = "Tomate", Descripcion = "Tomate fresco de la mejor calidad", IdCategoria = 7 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 16, Nombre = "Lechuga", Descripcion = "Lechuga fresca y crujiente", IdCategoria = 7 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 17, Nombre = "Pepino", Descripcion = "Pepino verde y fresco", IdCategoria = 7 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 18, Nombre = "Filete de Res", Descripcion = "Filete de res de alta calidad", IdCategoria = 8 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 19, Nombre = "Pechuga de Pollo", Descripcion = "Pechuga de pollo fresca y jugosa", IdCategoria = 8 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 20, Nombre = "Chuleta de Cerdo", Descripcion = "Chuleta de cerdo fresca y tierna", IdCategoria = 8 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 21, Nombre = "Arroz", Descripcion = "Arroz blanco de grano largo", IdCategoria = 9 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 22, Nombre = "Filete de salmón congelado", Descripcion = "Filete de salmón congelado al vacío", IdCategoria = 10 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 23, Nombre = "Pizza congelada de pepperoni", Descripcion = "Pizza congelada de pepperoni lista para hornear", IdCategoria = 10 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 24, Nombre = "Helado de vainilla", Descripcion = "Helado de vainilla en envase de 1 litro", IdCategoria = 10 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 25, Nombre = "Croquetas de pollo", Descripcion = "Croquetas de pollo congeladas, caja de 500g", IdCategoria = 10 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 26, Nombre = "Papas fritas congeladas", Descripcion = "Papas fritas congeladas, bolsa de 1kg", IdCategoria = 10 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 27, Nombre = "Vegetales mixtos congelados", Descripcion = "Mezcla de vegetales congelados, bolsa de 500g", IdCategoria = 10 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 28, Nombre = "Pan blanco", Descripcion = "Pan blanco fresco, barra de 500g", IdCategoria = 11 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 29, Nombre = "Croissant de mantequilla", Descripcion = "Croissant de mantequilla recién horneado", IdCategoria = 11 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 30, Nombre = "Chocolate con leche", Descripcion = "Tableta de chocolate con leche", IdCategoria = 12 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 31, Nombre = "Chocolate amargo", Descripcion = "Tableta de chocolate amargo, 70% cacao", IdCategoria = 12 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 32, Nombre = "Gomitas de frutas", Descripcion = "Bolsa de gomitas de frutas surtidas", IdCategoria = 12 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 33, Nombre = "Malvaviscos", Descripcion = "Bolsa de malvaviscos cubiertos de azúcar", IdCategoria = 12 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 34, Nombre = "Caramelos surtidos", Descripcion = "Bolsa de caramelos surtidos", IdCategoria = 12 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 35, Nombre = "Chicle de menta", Descripcion = "Paquete de chicles de menta, sin azúcar", IdCategoria = 12 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 36, Nombre = "Bombones de chocolate", Descripcion = "Caja de bombones de chocolate surtidos", IdCategoria = 12 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 37, Nombre = "Alfajores de dulce de leche", Descripcion = "Paquete de alfajores de dulce de leche, cubiertos de chocolate", IdCategoria = 12 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 38, Nombre = "Agua mineral", Descripcion = "Botella de agua mineral sin gas", IdCategoria = 14 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 39, Nombre = "Refresco de cola", Descripcion = "Botella de refresco de cola de 2 litros", IdCategoria = 14 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 40, Nombre = "Jugo de naranja", Descripcion = "Envase de jugo de naranja natural de 1 litro", IdCategoria = 14 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 41, Nombre = "Vino tinto reserva", Descripcion = "Botella de vino tinto reserva de 750 ml", IdCategoria = 13 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 42, Nombre = "Cerveza artesanal IPA", Descripcion = "Six-pack de cerveza artesanal India Pale Ale (IPA)", IdCategoria = 13 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 43, Nombre = "Shampoo fortificante", Descripcion = "Shampoo para fortalecer el cabello, envase de 400 ml", IdCategoria = 15 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 44, Nombre = "Crema hidratante facial", Descripcion = "Crema facial hidratante con ácido hialurónico, envase de 50 ml", IdCategoria = 15 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 45, Nombre = "Desodorante en barra", Descripcion = "Desodorante en barra para hombre, sin alcohol, envase de 50 g", IdCategoria = 15 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 46, Nombre = "Dental floss", Descripcion = "Hilo dental con menta, 50 metros", IdCategoria = 15 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 47, Nombre = "Auriculares inalámbricos", Descripcion = "Auriculares Bluetooth con cancelación de ruido, color negro", IdCategoria = 16 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 48, Nombre = "Smartwatch deportivo", Descripcion = "Reloj inteligente resistente al agua con GPS integrado, color rojo", IdCategoria = 16 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 49, Nombre = "Cargador inalámbrico", Descripcion = "Base de carga inalámbrica rápida para teléfonos compatibles, color blanco", IdCategoria = 16 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 50, Nombre = "Batería externa", Descripcion = "Power bank de 20000 mAh con puerto USB-C y USB-A, color gris", IdCategoria = 16 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 51, Nombre = "Altavoz Bluetooth", Descripcion = "Altavoz portátil con sonido estéreo de alta definición, color azul", IdCategoria = 16 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 52, Nombre = "Cámara de seguridad Wi-Fi", Descripcion = "Cámara IP para interiores con visión nocturna y detección de movimiento, color blanco", IdCategoria = 16 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 53, Nombre = "Teclado mecánico gaming", Descripcion = "Teclado retroiluminado con switches mecánicos y anti-ghosting, color negro", IdCategoria = 16 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 54, Nombre = "Mouse óptico", Descripcion = "Mouse ergonómico con sensor óptico de alta precisión, color gris", IdCategoria = 16 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 55, Nombre = "Router Wi-Fi 6", Descripcion = "Router inalámbrico de última generación con velocidad de hasta 6000 Mbps, color negro", IdCategoria = 16 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 56, Nombre = "Tableta gráfica", Descripcion = "Tableta digitalizadora con lápiz sensible a la presión, ideal para diseño gráfico, color blanco", IdCategoria = 16 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 57, Nombre = "Lámpara inteligente", Descripcion = "Bombilla LED inteligente controlable por voz y aplicación móvil, color blanco cálido", IdCategoria = 16 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 58, Nombre = "Camisa de vestir", Descripcion = "Camisa de algodón con cuello clásico y botones frontales, color blanco", IdCategoria = 17 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 59, Nombre = "Pantalones vaqueros", Descripcion = "Jeans de corte recto y diseño clásico, color azul índigo", IdCategoria = 17 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 60, Nombre = "Vestido de fiesta", Descripcion = "Vestido largo de satén con escote en V y detalle de encaje, color negro", IdCategoria = 17 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 61, Nombre = "Zapatillas deportivas", Descripcion = "Tenis para correr con amortiguación y suela de goma, color gris y rosa", IdCategoria = 17 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 62, Nombre = "Bufanda de lana", Descripcion = "Bufanda suave y abrigada tejida a mano, color gris jaspeado", IdCategoria = 17 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 63, Nombre = "Libro de cocina: Recetas tradicionales", Descripcion = "Colección de recetas auténticas de diversas regiones del mundo", IdCategoria = 18 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 64, Nombre = "Revista de moda: Últimas tendencias", Descripcion = "Edición mensual con los estilos más actuales y consejos de moda", IdCategoria = 18 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 65, Nombre = "Muñeca articulada", Descripcion = "Muñeca de 30 cm con ropa intercambiable y accesorios", IdCategoria = 19 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 66, Nombre = "Circuito de carreras", Descripcion = "Set de carreras con pistas, coches y accesorios para montar", IdCategoria = 19 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 67, Nombre = "Rompecabezas de 1000 piezas", Descripcion = "Rompecabezas de paisajes naturales para armar en familia", IdCategoria = 19 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 68, Nombre = "Set de bloques de construcción", Descripcion = "Set de 200 bloques de colores para construir y crear", IdCategoria = 19 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 69, Nombre = "Peluche gigante de oso", Descripcion = "Peluche suave de 1 metro de altura para abrazar", IdCategoria = 19 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 70, Nombre = "Kit de manualidades para niños", Descripcion = "Set con materiales para crear manualidades y proyectos artísticos", IdCategoria = 19 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 71, Nombre = "Juego de mesa: Aventuras en la selva", Descripcion = "Juego de mesa interactivo para explorar la selva y superar desafíos", IdCategoria = 19 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 72, Nombre = "Juego de destornilladores", Descripcion = "Set de 6 destornilladores de diferentes tamaños y puntas", IdCategoria = 20 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 73, Nombre = "Taladro eléctrico inalámbrico", Descripcion = "Taladro con batería recargable, ideal para trabajos domésticos", IdCategoria = 20 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 74, Nombre = "Caja de herramientas con ruedas", Descripcion = "Caja resistente con compartimentos y ruedas para transportar herramientas", IdCategoria = 20 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 75, Nombre = "Cuadro abstracto moderno", Descripcion = "Cuadro decorativo con diseño abstracto y marco de madera", IdCategoria = 21 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 76, Nombre = "Cojines decorativos", Descripcion = "Set de 4 cojines con diferentes diseños y colores para sofá o cama", IdCategoria = 21 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 77, Nombre = "Jarrón de cerámica", Descripcion = "Jarrón decorativo de cerámica con diseño elegante para flores", IdCategoria = 21 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 78, Nombre = "Espejo de pared rectangular", Descripcion = "Espejo de pared con marco de metal negro, ideal para entrada o sala", IdCategoria = 21 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 79, Nombre = "Llantas deportivas", Descripcion = "Llantas de alta performance para vehículos deportivos", IdCategoria = 22 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 80, Nombre = "Kit de luces LED para automóviles", Descripcion = "Kit completo de luces LED para mejorar la iluminación del vehículo", IdCategoria = 22 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 81, Nombre = "Podadora de césped eléctrica", Descripcion = "Podadora eléctrica para cortar el césped de jardines pequeños y medianos", IdCategoria = 23 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 82, Nombre = "Collar ajustable para perros", Descripcion = "Collar resistente y ajustable para perros de todos los tamaños", IdCategoria = 24 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 83, Nombre = "Juguete para gatos con plumas", Descripcion = "Juguete interactivo para gatos con plumas y sonidos que estimulan su instinto de caza", IdCategoria = 24 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 84, Nombre = "Comedero automático para mascotas", Descripcion = "Comedero automático programable para perros y gatos que dispensa comida en horarios predefinidos", IdCategoria = 24 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 85, Nombre = "Guitarra acústica", Descripcion = "Guitarra acústica de calidad con cuerdas de nylon para principiantes", IdCategoria = 25 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 86, Nombre = "Teclado electrónico", Descripcion = "Teclado electrónico de 61 teclas con múltiples funciones y sonidos", IdCategoria = 25 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 87, Nombre = "Batería completa para niños", Descripcion = "Batería infantil de tamaño compacto con todo lo necesario para empezar a tocar", IdCategoria = 25 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 88, Nombre = "Violín profesional", Descripcion = "Violín de alta calidad hecho a mano con madera de abeto y arce", IdCategoria = 25 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 89, Nombre = "Micrófono de condensador", Descripcion = "Micrófono de condensador con soporte y filtro anti-pop para grabaciones profesionales", IdCategoria = 25 });
            modelBuilder.Entity<Producto>().HasData(new Producto { Id = 90, Nombre = "Piano digital de escenario", Descripcion = "Piano digital portátil con teclado contrapesado y múltiples sonidos de alta calidad", IdCategoria = 25 });

            DateTime RandomDate(DateTime startDate, DateTime endDate)
            {
                Random random = new Random();
                int range = (endDate - startDate).Days;
                return startDate.AddDays(random.Next(range));
            }

            //Insertar datos entrada
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 1, IdCategoria = 6, IdProducto = 11, IdProveedor = 5, PrecioCompra = 900, PrecioVenta = 1500, ExistenciaInicial = 50, ExistenciaActual = 16, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 2, IdCategoria = 8, IdProducto = 20, IdProveedor = 7, PrecioCompra = 3500, PrecioVenta = 6000, ExistenciaInicial = 150, ExistenciaActual = 50, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 3, IdCategoria = 10, IdProducto = 23, IdProveedor = 9, PrecioCompra = 10000, PrecioVenta = 18900, ExistenciaInicial = 50, ExistenciaActual = 43, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 4, IdCategoria = 10, IdProducto = 26, IdProveedor = 9, PrecioCompra = 6000, PrecioVenta = 12000, ExistenciaInicial = 200, ExistenciaActual = 200, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 5, IdCategoria = 11, IdProducto = 29, IdProveedor = 10, PrecioCompra = 2300, PrecioVenta = 3000, ExistenciaInicial = 50, ExistenciaActual = 45, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 6, IdCategoria = 12, IdProducto = 31, IdProveedor = 11, PrecioCompra = 1200, PrecioVenta = 3000, ExistenciaInicial = 400, ExistenciaActual = 400, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 7, IdCategoria = 12, IdProducto = 34, IdProveedor = 11, PrecioCompra = 5000, PrecioVenta = 8000, ExistenciaInicial = 100, ExistenciaActual = 65, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 8, IdCategoria = 12, IdProducto = 35, IdProveedor = 11, PrecioCompra = 2500, PrecioVenta = 3400, ExistenciaInicial = 300, ExistenciaActual = 290, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 9, IdCategoria = 13, IdProducto = 42, IdProveedor = 12, PrecioCompra = 2000, PrecioVenta = 3800, ExistenciaInicial = 70, ExistenciaActual = 67, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 10, IdCategoria = 14, IdProducto = 40, IdProveedor = 13, PrecioCompra = 1300, PrecioVenta = 2600, ExistenciaInicial = 90, ExistenciaActual = 81, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 11, IdCategoria = 15, IdProducto = 45, IdProveedor = 14, PrecioCompra = 4000, PrecioVenta = 6300, ExistenciaInicial = 200, ExistenciaActual = 200, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 12, IdCategoria = 15, IdProducto = 44, IdProveedor = 14, PrecioCompra = 4500, PrecioVenta = 10000, ExistenciaInicial = 135, ExistenciaActual = 135, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 13, IdCategoria = 16, IdProducto = 47, IdProveedor = 15, PrecioCompra = 18000, PrecioVenta = 34999, ExistenciaInicial = 80, ExistenciaActual = 80, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 14, IdCategoria = 16, IdProducto = 48, IdProveedor = 15, PrecioCompra = 790000, PrecioVenta = 990000, ExistenciaInicial = 100, ExistenciaActual = 100, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 15, IdCategoria = 16, IdProducto = 49, IdProveedor = 15, PrecioCompra = 45000, PrecioVenta = 60000, ExistenciaInicial = 50, ExistenciaActual = 50, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 16, IdCategoria = 16, IdProducto = 50, IdProveedor = 15, PrecioCompra = 100000, PrecioVenta = 170000, ExistenciaInicial = 35, ExistenciaActual = 35, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 17, IdCategoria = 16, IdProducto = 53, IdProveedor = 15, PrecioCompra = 1700000, PrecioVenta = 1980000, ExistenciaInicial = 300, ExistenciaActual = 300, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 18, IdCategoria = 16, IdProducto = 55, IdProveedor = 15, PrecioCompra = 178000, PrecioVenta = 222000, ExistenciaInicial = 50, ExistenciaActual = 50, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 19, IdCategoria = 17, IdProducto = 59, IdProveedor = 16, PrecioCompra = 60000, PrecioVenta = 99000, ExistenciaInicial = 50, ExistenciaActual = 50, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 20, IdCategoria = 17, IdProducto = 61, IdProveedor = 16, PrecioCompra = 800000, PrecioVenta = 1099999, ExistenciaInicial = 40, ExistenciaActual = 40, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 21, IdCategoria = 18, IdProducto = 63, IdProveedor = 17, PrecioCompra = 46000, PrecioVenta = 80000, ExistenciaInicial = 38, ExistenciaActual = 38, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 22, IdCategoria = 19, IdProducto = 66, IdProveedor = 18, PrecioCompra = 400000, PrecioVenta = 470000, ExistenciaInicial = 200, ExistenciaActual = 200, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 23, IdCategoria = 19, IdProducto = 67, IdProveedor = 18, PrecioCompra = 20000, PrecioVenta = 32000, ExistenciaInicial = 50, ExistenciaActual = 50, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 24, IdCategoria = 20, IdProducto = 73, IdProveedor = 19, PrecioCompra = 267000, PrecioVenta = 450000, ExistenciaInicial = 70, ExistenciaActual = 70, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 25, IdCategoria = 21, IdProducto = 75, IdProveedor = 20, PrecioCompra = 800000, PrecioVenta = 1700000, ExistenciaInicial = 200, ExistenciaActual = 200, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 26, IdCategoria = 21, IdProducto = 77, IdProveedor = 20, PrecioCompra = 125000, PrecioVenta = 230000, ExistenciaInicial = 50, ExistenciaActual = 50, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 27, IdCategoria = 21, IdProducto = 78, IdProveedor = 20, PrecioCompra = 70000, PrecioVenta = 130000, ExistenciaInicial = 70, ExistenciaActual = 70, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 28, IdCategoria = 22, IdProducto = 80, IdProveedor = 21, PrecioCompra = 90000, PrecioVenta = 136000, ExistenciaInicial = 200, ExistenciaActual = 200, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 29, IdCategoria = 23, IdProducto = 81, IdProveedor = 22, PrecioCompra = 1800000, PrecioVenta = 2780000, ExistenciaInicial = 50, ExistenciaActual = 50, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 30, IdCategoria = 24, IdProducto = 84, IdProveedor = 23, PrecioCompra = 270000, PrecioVenta = 400000, ExistenciaInicial = 70, ExistenciaActual = 57, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 31, IdCategoria = 25, IdProducto = 87, IdProveedor = 24, PrecioCompra = 2000000, PrecioVenta = 2800000, ExistenciaInicial = 50, ExistenciaActual = 34, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 32, IdCategoria = 25, IdProducto = 88, IdProveedor = 24, PrecioCompra = 2300000, PrecioVenta = 2900000, ExistenciaInicial = 70, ExistenciaActual = 68, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 33, IdCategoria = 25, IdProducto = 89, IdProveedor = 24, PrecioCompra = 800000, PrecioVenta = 1400000, ExistenciaInicial = 35, ExistenciaActual = 17, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 34, IdCategoria = 25, IdProducto = 85, IdProveedor = 24, PrecioCompra = 2400000, PrecioVenta = 3800000, ExistenciaInicial = 50, ExistenciaActual = 37, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 35, IdCategoria = 5, IdProducto = 5, IdProveedor = 4, PrecioCompra = 1800, PrecioVenta = 3500, ExistenciaInicial = 70, ExistenciaActual = 70, Nota = "", FechaEntrada = RandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 12)), Estado = "Activo" });


            Random random = new Random();
            DateTime fechaAleatoria = new DateTime(
                random.Next(2024, DateTime.Now.Year),  // Año aleatorio entre 2010 y el año actual
                random.Next(1, 12),                        // Mes aleatorio entre 1 y 12
                random.Next(1, 28),                        // Día aleatorio entre 1 y 28 (para evitar problemas con febrero)
                random.Next(0, 23),                        // Hora aleatoria entre 0 y 23
                random.Next(0, 59),                        // Minuto aleatorio entre 0 y 59
                0);

            //Insertar datos salida
            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 1, FechaFactura = fechaAleatoria, IdCliente = 3, CantidadProductos = 2, TotalPagarConDescuento = 607500, TotalPagarSinDescuento = 607500, TotalDescuento = 0 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 1, IdSalida = 1, IdCategoria = 6, IdProducto = 11, IdEntrada = 1, Precio = 1500, Cantidad = 5, Descuento = 0, ValorDescuento = 0, Total = 7500 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 2, IdSalida = 1, IdCategoria = 8, IdProducto = 20, IdEntrada = 2, Precio = 6000, Cantidad = 100, Descuento = 0, ValorDescuento = 0, Total = 600000 });

            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 2, FechaFactura = fechaAleatoria, IdCliente = 1, CantidadProductos = 1, TotalPagarConDescuento = 28500, TotalPagarSinDescuento = 30000, TotalDescuento = 1500 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 3, IdSalida = 2, IdCategoria = 6, IdProducto = 11, IdEntrada = 1, Precio = 1500, Cantidad = 20, Descuento = 5, ValorDescuento = 1500, Total = 28500 });

            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 3, FechaFactura = fechaAleatoria, IdCliente = 5, CantidadProductos = 4, TotalPagarConDescuento = 186400, TotalPagarSinDescuento = 188800, TotalDescuento = 2400 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 4, IdSalida = 3, IdCategoria = 12, IdProducto = 34, IdEntrada = 7, Precio = 8000, Cantidad = 15, Descuento = 2, ValorDescuento = 2400, Total = 117600 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 5, IdSalida = 3, IdCategoria = 12, IdProducto = 35, IdEntrada = 8, Precio = 3400, Cantidad = 10, Descuento = 0, ValorDescuento = 0, Total = 34000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 6, IdSalida = 3, IdCategoria = 13, IdProducto = 42, IdEntrada = 9, Precio = 3800, Cantidad = 3, Descuento = 0, ValorDescuento = 0, Total = 11400 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 7, IdSalida = 3, IdCategoria = 14, IdProducto = 40, IdEntrada = 10, Precio = 2600, Cantidad = 9, Descuento = 0, ValorDescuento = 0, Total = 23400 });

            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 4, FechaFactura = fechaAleatoria, IdCliente = 1, CantidadProductos = 1, TotalPagarConDescuento = 132300, TotalPagarSinDescuento = 132300, TotalDescuento = 0 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 8, IdSalida = 4, IdCategoria = 10, IdProducto = 23, IdEntrada = 3, Precio = 18900, Cantidad = 7, Descuento = 0, ValorDescuento = 0, Total = 132300 });

            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 5, FechaFactura = fechaAleatoria, IdCliente = 11, CantidadProductos = 1, TotalPagarConDescuento = 4500, TotalPagarSinDescuento = 4500, TotalDescuento = 0 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 9, IdSalida = 5, IdCategoria = 6, IdProducto = 11, IdEntrada = 1, Precio = 1500, Cantidad = 3, Descuento = 0, ValorDescuento = 0, Total = 4500 });

            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 6, FechaFactura = fechaAleatoria, IdCliente = 3, CantidadProductos = 1, TotalPagarConDescuento = 144000, TotalPagarSinDescuento = 160000, TotalDescuento = 16000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 10, IdSalida = 6, IdCategoria = 12, IdProducto = 34, IdEntrada = 7, Precio = 8000, Cantidad = 20, Descuento = 10, ValorDescuento = 16000, Total = 144000 });

            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 7, FechaFactura = fechaAleatoria, IdCliente = 7, CantidadProductos = 6, TotalPagarConDescuento = 46052000, TotalPagarSinDescuento = 46052000, TotalDescuento = 0 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 11, IdSalida = 7, IdCategoria = 24, IdProducto = 84, IdEntrada = 30, Precio = 400000, Cantidad = 3, Descuento = 0, ValorDescuento = 0, Total = 1200000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 12, IdSalida = 7, IdCategoria = 25, IdProducto = 87, IdEntrada = 31, Precio = 2800000, Cantidad = 6, Descuento = 0, ValorDescuento = 0, Total = 16800000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 13, IdSalida = 7, IdCategoria = 25, IdProducto = 88, IdEntrada = 32, Precio = 2900000, Cantidad = 2, Descuento = 0, ValorDescuento = 0, Total = 5800000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 14, IdSalida = 7, IdCategoria = 25, IdProducto = 89, IdEntrada = 33, Precio = 1400000, Cantidad = 5, Descuento = 0, ValorDescuento = 0, Total = 7000000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 15, IdSalida = 7, IdCategoria = 25, IdProducto = 85, IdEntrada = 34, Precio = 3800000, Cantidad = 4, Descuento = 0, ValorDescuento = 0, Total = 15200000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 16, IdSalida = 7, IdCategoria = 11, IdProducto = 29, IdEntrada = 5, Precio = 2600, Cantidad = 20, Descuento = 0, ValorDescuento = 0, Total = 52000 });

            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 8, FechaFactura = fechaAleatoria, IdCliente = 5, CantidadProductos = 2, TotalPagarConDescuento = 28800000, TotalPagarSinDescuento = 32000000, TotalDescuento = 3200000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 17, IdSalida = 8, IdCategoria = 24, IdProducto = 84, IdEntrada = 30, Precio = 400000, Cantidad = 10, Descuento = 10, ValorDescuento = 400000, Total = 3600000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 18, IdSalida = 8, IdCategoria = 25, IdProducto = 87, IdEntrada = 31, Precio = 2800000, Cantidad = 10, Descuento = 10, ValorDescuento = 2800000, Total = 25200000 });

            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 9, FechaFactura = fechaAleatoria, IdCliente = 2, CantidadProductos = 1, TotalPagarConDescuento = 9000, TotalPagarSinDescuento = 9000, TotalDescuento = 0 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 19, IdSalida = 9, IdCategoria = 6, IdProducto = 11, IdEntrada = 1, Precio = 1500, Cantidad = 6, Descuento = 0, ValorDescuento = 0, Total = 9000 });

            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 10, FechaFactura = fechaAleatoria, IdCliente = 13, CantidadProductos = 2, TotalPagarConDescuento = 52400000, TotalPagarSinDescuento = 52400000, TotalDescuento = 0 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 20, IdSalida = 10, IdCategoria = 25, IdProducto = 89, IdEntrada = 33, Precio = 1400000, Cantidad = 13, Descuento = 0, ValorDescuento = 0, Total = 18200000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 21, IdSalida = 10, IdCategoria = 25, IdProducto = 85, IdEntrada = 34, Precio = 3800000, Cantidad = 9, Descuento = 0, ValorDescuento = 0, Total = 34200000 });

            ////Insertar datos Reporte
            //modelBuilder.Entity<Reporte>().HasData(new Reporte { Id = 1, Nombre = "Reporte general de ventas", Descripcion = "Reporte general de las ventas" });
            //modelBuilder.Entity<Reporte>().HasData(new Reporte { Id = 2, Nombre = "Reporte entre dos fechas", Descripcion = "seleciona un rango de fechas para realizar un reporte de ventas en esa fecha" });

        }
    }
}
