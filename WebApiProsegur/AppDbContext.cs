using Microsoft.EntityFrameworkCore;
using WebApiProsegur.Entidades;

namespace WebApiProsegur
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProvinciaEntity>().HasData(
                  new ProvinciaEntity { Id = 1, Nombre = "Lima", Impuesto = 18 },
                  new ProvinciaEntity { Id = 2, Nombre = "Lima Norte", Impuesto = 16 }
           );
            modelBuilder.Entity<TiendasEntity>().HasData(
                   new TiendasEntity { Id = 1, Nombre = "Lima Sur", IdProvincia = 1 },
                   new TiendasEntity { Id = 2, Nombre = "Lima Norte", IdProvincia = 1 },
                   new TiendasEntity { Id = 3, Nombre = "Chimbote", IdProvincia = 2 }
            );
            modelBuilder.Entity<RolEntity>().HasData(
              new RolEntity { Id = 1, Nombre = "Administrador" },
              new RolEntity { Id = 2, Nombre = "Supervisor" },
              new RolEntity { Id = 3, Nombre = "Empleado" },
              new RolEntity { Id = 4, Nombre = "Usuario" }
             );
            modelBuilder.Entity<ItemsEntity>().HasData(
             new ItemsEntity { Id = 1, Nombre = "Cafe Americano", Tiempo = 3, Precio = 16 },
             new ItemsEntity { Id = 2, Nombre = "Cafe Mocca", Tiempo = 4, Precio = 18 },
             new ItemsEntity { Id = 3, Nombre = "Cafe con Leche", Tiempo = 5, Precio = 15 },
             new ItemsEntity { Id = 4, Nombre = "Infusión", Tiempo = 3, Precio = 10 }
            );

            modelBuilder.Entity<MateriaPrimaEntity>().HasData(
             new MateriaPrimaEntity { Id = 1, Nombre = "Cafe expreso", Cantidad = 15 },
             new MateriaPrimaEntity { Id = 2, Nombre = "Leche", Cantidad = 15 },
             new MateriaPrimaEntity { Id = 3, Nombre = "Crema batidad", Cantidad = 15 },
             new MateriaPrimaEntity { Id = 4, Nombre = "Anis", Cantidad = 0 },
             new MateriaPrimaEntity { Id = 5, Nombre = "Azucar", Cantidad = 7 }
            );

            modelBuilder.Entity<ItemsMaterialesEntity>().HasData(
            new ItemsMaterialesEntity { Id = 1, IdItems = 1, IdMaterial = 1 },
            new ItemsMaterialesEntity { Id = 2, IdItems = 1, IdMaterial = 5 },
            new ItemsMaterialesEntity { Id = 3, IdItems = 2, IdMaterial = 1 },
            new ItemsMaterialesEntity { Id = 4, IdItems = 2, IdMaterial = 2 },
            new ItemsMaterialesEntity { Id = 5, IdItems = 2, IdMaterial = 3 },
            new ItemsMaterialesEntity { Id = 6, IdItems = 2, IdMaterial = 5 },
            new ItemsMaterialesEntity { Id = 7, IdItems = 3, IdMaterial = 1 },
            new ItemsMaterialesEntity { Id = 8, IdItems = 3, IdMaterial = 2 },
            new ItemsMaterialesEntity { Id = 9, IdItems = 3, IdMaterial = 5 },
            new ItemsMaterialesEntity { Id = 10, IdItems = 4, IdMaterial = 4 },
            new ItemsMaterialesEntity { Id = 11, IdItems = 4, IdMaterial = 5 }
           );

            modelBuilder.Entity<OrdenEntity>().HasData(
            new OrdenEntity { Id = 1, IdUsuario = 2, IdTienda = 1, Cliente = "Susan", Estado = 1, SubTotal = 104, Impuesto = 18.72, Total = 122.72 },
            new OrdenEntity { Id = 2, IdUsuario = 2, IdTienda = 1, Cliente = "Daniel", Estado = 1, SubTotal = 129, Impuesto = 23.22, Total = 152.22 }
            );

            modelBuilder.Entity<DetalleOrdenEntity>().HasData(
           new DetalleOrdenEntity { Id = 1, IdOrden = 1, IdItems = 1, Cantidad = 2 },
           new DetalleOrdenEntity { Id = 2, IdOrden = 1, IdItems = 2, Cantidad = 4 },
           new DetalleOrdenEntity { Id = 3, IdOrden = 2, IdItems = 3, Cantidad = 5 },
           new DetalleOrdenEntity { Id = 4, IdOrden = 2, IdItems = 2, Cantidad = 3 }

           );
        }

        public DbSet<RolEntity> Roles { get; set; }
        public DbSet<ProvinciaEntity> Provincias { get; set; }
        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<TiendasEntity> Tiendas { get; set; }
        public DbSet<ItemsEntity> Items { get; set; }
        public DbSet<MateriaPrimaEntity> Materiales { get; set; }
        public DbSet<ItemsMaterialesEntity> DetalleItems { get; set; }
        public DbSet<OrdenEntity> Ordenes { get; set; }
        public DbSet<DetalleOrdenEntity> DetalleOrdenes { get; set; }
    }
}
