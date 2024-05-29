using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tekus.Domain.Entities
{
    public class Provider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idprovider { get; set; }

        [Required]
        public string nit { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string mail { get; set; }
        public ICollection<ProviderHasServices> ProviderHasServices { get; set; }

    }
}
