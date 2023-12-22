using System.ComponentModel.DataAnnotations;

namespace retoAPI.Models
{
    public class Puesto
    {
        public int Id { get; set; }

        [Required]
        public string NombrePuesto { get; set; }

        // Relación con Empleado
        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
