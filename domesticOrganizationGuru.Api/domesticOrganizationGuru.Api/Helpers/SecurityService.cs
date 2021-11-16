using System;
using System.Security.Cryptography;
using System.Text;

namespace DomesticOrganizationGuru.Api.Helpers
{
    public static class SecurityService
    {
        public static string StringSha256Hash(string text) =>
            string.IsNullOrEmpty(text) ? 
            string.Empty : 
            BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", string.Empty);
    }
}
