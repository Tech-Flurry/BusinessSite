using Newtonsoft.Json;

namespace TechFlurry.BusinessSite.Models
{
    public class DocumentModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
    }
}
