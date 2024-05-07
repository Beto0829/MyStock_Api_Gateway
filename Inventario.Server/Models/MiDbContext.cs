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
              .HasOne(e => e.proveedor)            // Una compra tiene un proveedor
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

            //Insertar datos cliente
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 1, Nombre = "Juan Jose", Celular = "3102345690", Correo = "josejuandios@gmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 2, Nombre = "Brayam", Celular = "3114315673", Correo = "jumancitopues@hotmail.com" });
            modelBuilder.Entity<Cliente>().HasData(new Cliente { Id = 3, Nombre = "Kevin Julio", Celular = "3005554307", Correo = "kjulio1212@outlook.com" });

            //Insertar datos proveedors
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 1, Nombre = "Postobon", Celular = "3187779090", Correo = "postocolombia@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 2, Nombre = "Jamar", Celular = "3145623143", Correo = "mueblesjamar@hotmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 3, Nombre = "Colanta", Celular = "3005554307", Correo = "colantaleche@gmail.com" });
            modelBuilder.Entity<Proveedor>().HasData(new Proveedor { Id = 4, Nombre = "Adidas", Celular = "3005567889", Correo = "adidascolombia@gmail.com" });

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

            //Insertar datos entrada
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 1, IdCategoria = 3, IdProducto = 3, IdProveedor = 3, PrecioCompra = 5800, PrecioVenta = 8000, ExistenciaInicial = 50, ExistenciaActual = 30, Nota = "Yogurt marca colanta sabores fresa y melocoton", FechaEntrada = DateTime.Now, Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 2, IdCategoria = 5, IdProducto = 7, IdProveedor = 1, PrecioCompra = 1800, PrecioVenta = 2600, ExistenciaInicial = 200, ExistenciaActual = 80, Nota = "La idea es venderlas en maximo un mes", FechaEntrada = DateTime.Now, Estado = "Activo" });
            modelBuilder.Entity<Entrada>().HasData(new Entrada { Id = 3, IdCategoria = 2, IdProducto = 8, IdProveedor = 4, PrecioCompra = 50000, PrecioVenta = 190000, ExistenciaInicial = 60, ExistenciaActual = 40, Nota = "Producto al cual sacarle mucho provecho por su precio de compra y de venta", FechaEntrada = DateTime.Now, Estado = "Activo" });

            DateTime fechaHoraActual = DateTime.Now;
            //Insertar datos salida
            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 1, FechaFactura = new DateTime(fechaHoraActual.Year, fechaHoraActual.Month, fechaHoraActual.Day, fechaHoraActual.Hour, fechaHoraActual.Minute, 0), IdCliente = 3, CantidadProductos = 2, TotalPagarConDescuento = 274000, TotalPagarSinDescuento = 300000, TotalDescuento = 26000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 1, IdSalida = 1, IdCategoria = 3, IdProducto = 3, IdEntrada = 1, Precio = 8000, Cantidad = 5, Descuento = 0, ValorDescuento = 0, Total = 40000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 2, IdSalida = 1, IdCategoria = 5, IdProducto = 7, IdEntrada = 2, Precio = 2600, Cantidad = 100, Descuento = 10, ValorDescuento = 26000, Total = 234000 });

            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 2, FechaFactura = new DateTime(fechaHoraActual.Year, fechaHoraActual.Month, fechaHoraActual.Day, fechaHoraActual.Hour, fechaHoraActual.Minute, 0), IdCliente = 1, CantidadProductos = 1, TotalPagarConDescuento = 3610000, TotalPagarSinDescuento = 3800000, TotalDescuento = 190000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 3, IdSalida = 2, IdCategoria = 2, IdProducto = 8, IdEntrada = 3, Precio = 190000, Cantidad = 20, Descuento = 5, ValorDescuento = 190000, Total = 3610000 });

            modelBuilder.Entity<Salida>().HasData(new Salida { Id = 3, FechaFactura = new DateTime(fechaHoraActual.Year, fechaHoraActual.Month, fechaHoraActual.Day, fechaHoraActual.Hour, fechaHoraActual.Minute, 0), IdCliente = 2, CantidadProductos = 2, TotalPagarConDescuento = 164960, TotalPagarSinDescuento = 172000, TotalDescuento = 7040 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 4, IdSalida = 3, IdCategoria = 3, IdProducto = 3, IdEntrada = 1, Precio = 8000, Cantidad = 15, Descuento = 5, ValorDescuento = 6000, Total = 114000 });
            modelBuilder.Entity<ProductoSalida>().HasData(new ProductoSalida { Id = 5, IdSalida = 3, IdCategoria = 5, IdProducto = 7, IdEntrada = 2, Precio = 2600, Cantidad = 20, Descuento = 2, ValorDescuento = 1040, Total = 50960 });

            ////Insertar datos Reporte
            //modelBuilder.Entity<Reporte>().HasData(new Reporte { Id = 1, Nombre = "Reporte general de ventas", Descripcion = "Reporte general de las ventas" });
            //modelBuilder.Entity<Reporte>().HasData(new Reporte { Id = 2, Nombre = "Reporte entre dos fechas", Descripcion = "seleciona un rango de fechas para realizar un reporte de ventas en esa fecha" });

        }        
    }
}
