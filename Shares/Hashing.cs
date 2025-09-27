using System.Security.Cryptography;
using System.Text;

namespace InventoryV2.Shares
{
    public static class Hashing
    {
        public static string Generate(string originalString)
        {
            // Convert the string to a byte array
            byte[] originalBytes = Encoding.UTF8.GetBytes(originalString);

            // Compute the SHA256 hash
            using SHA256 sha256Hash =  SHA256.Create();
            byte[] hashBytes = sha256Hash.ComputeHash(originalBytes);

            // Convert the hash byte array to a hexadecimal string for display
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++) {
                builder.Append(hashBytes[i].ToString("x2")); // "x2" formats as two hexadecimal digits
            }
            string hashString = builder.ToString();

            return hashString;

        }
    }
}
