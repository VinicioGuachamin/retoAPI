using System.ComponentModel.DataAnnotations;

namespace retoAPI.Models
{
    public class Parentesco
    {
        public int Id { get; set; }

        [Required]
        public string Descripcion { get; set; }

        // Relación con Dependiente
        public virtual ICollection<Dependiente> Dependientes { get; set; }
    }
}
