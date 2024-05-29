
namespace Tekus.Domain.Entities
{
    public class ProviderHasServices
    {
        public int Providers_idprovider { get; set; }
        public Provider Provider { get; set; }

        public int Services_idservice { get; set; }
        public Services Services { get; set; }

        public int Country_idcountry { get; set; }
        public Country Country { get; set; }
        public float time_value { get; set; }

    }
}
