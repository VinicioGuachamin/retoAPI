using Microsoft.EntityFrameworkCore;
using retoAPI.Models;

namespace retoAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<Dependiente> Dependientes { get; set; }
        public DbSet<Parentesco> Parentescos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales

            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Puesto)
                .WithMany(p => p.Empleados)
                .HasForeignKey(e => e.PuestoId);

            modelBuilder.Entity<Dependiente>()
                .HasOne(d => d.Empleado)
                .WithMany(e => e.Dependientes)
                .HasForeignKey(d => d.EmpleadoId);

            modelBuilder.Entity<Dependiente>()
                .HasOne(d => d.Parentesco)
                .WithMany(p => p.Dependientes)
                .HasForeignKey(d => d.ParentescoId);
        }
    }
}
