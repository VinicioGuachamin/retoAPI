using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace retoAPI.Models
{
    public class Dependiente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoDependiente { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public int ParentescoId { get; set; }

        [ForeignKey("ParentescoId")]
        public virtual Parentesco? Parentesco { get; set; }

        // Relación con Empleado
        public int EmpleadoId { get; set; }

        [ForeignKey("EmpleadoId")]
        public virtual Empleado? Empleado { get; set; }
    }
}
