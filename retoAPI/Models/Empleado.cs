using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace retoAPI.Models
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoEmpleado { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalarioFijo { get; set; }

        public bool? PagaImpuestoRenta { get; set; }

        [Required]
        public int PuestoId { get; set; }

        [ForeignKey("PuestoId")]
        public virtual Puesto? Puesto { get; set; }

        [RegularExpression("[A-C]", ErrorMessage = "El código de impuesto sobre la renta solo puede ser A, B o C.")]
        public string CodigoImpuestoRenta { get; set; }

        public int? NumeroDependientes { get; set; }
        public virtual ICollection<Dependiente>? Dependientes { get; set; }
    }
}
