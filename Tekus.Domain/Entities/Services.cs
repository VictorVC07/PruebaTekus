using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Tekus.Domain.Entities
{
    public class Services
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idservice { get; set; }

        [Required]
        public string service { get; set; }

        [Required]
        public float time_value { get; set; }
        public ICollection<ProviderHasServices> ProviderHasServices { get; set; }
    }
}
