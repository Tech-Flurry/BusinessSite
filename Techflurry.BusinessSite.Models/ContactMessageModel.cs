using Newtonsoft.Json;

namespace TechFlurry.BusinessSite.Models
{
    public class ContactMessageModel : DocumentModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Service { get; set; }
        public string Message { get; set; }
    }
}
