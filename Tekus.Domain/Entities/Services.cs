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

        public List<ProviderHasServices> ProviderHasServices { get; set; } = new List<ProviderHasServices>();
    }
}
