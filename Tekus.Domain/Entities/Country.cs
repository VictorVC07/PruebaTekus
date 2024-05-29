﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tekus.Domain.Entities
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idcountry { get; set; }
        public string country { get; set; }
        public ICollection<ProviderHasServices> ProviderHasServices { get; set; }
    }
}
