using Newtonsoft.Json;
using System.Security.Claims;

namespace TechFlurry.BusinessSite.App.Authentication
{
    internal static class Extensions
    {
        public static string GetName( this IEnumerable<Claim> claims )
        {
            var name = claims?.FirstOrDefault(x => x.Type == "name")?.Value;
            return name ?? string.Empty;
        }
        public static string GetCountry( this IEnumerable<Claim> claims )
        {
            var country = claims?.FirstOrDefault(x => x.Type == "country")?.Value;
            return country ?? string.Empty;
        }
        public static string GetAppId( this IEnumerable<Claim> claims )
        {
            var country = claims?.FirstOrDefault(x => x.Type == "aud")?.Value;
            return country ?? string.Empty;
        }
        public static string GetBusinessName( this IEnumerable<Claim> claims )
        {
            var businessName = claims?.FirstOrDefault(x => x.Type == "extension_BusinessName")?.Value;
            return businessName ?? string.Empty;
        }
        public static List<string> GetEmails( this IEnumerable<Claim> claims )
        {
            var emailsJson = claims?.FirstOrDefault(x => x.Type == "emails")?.Value;
            var emails = JsonConvert.DeserializeObject<List<string>>(emailsJson);
            return emails ?? new List<string>();
        }
        public static Guid GetUserId( this IEnumerable<Claim> claims )
        {
            Guid userId = Guid.Parse(claims?.FirstOrDefault(x => x.Type == "oid")?.Value);
            return userId;
        }
        public static bool IsNewUser( this IEnumerable<Claim> claims )
        {
            var isNewUser = bool.Parse(claims?.FirstOrDefault(x => x.Type == "newUser")?.Value);
            return isNewUser;
        }
    }
}
