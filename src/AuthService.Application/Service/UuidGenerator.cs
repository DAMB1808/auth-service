using System.Security.Cryptography;
using System.text;

namespace AuthService.Application.Services;

private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static class UuidGenerator
    {

        public static string GenerateShortUUID()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[12];
            rng.GetBytes(bytes);

            var result = new StringBuilder(12);

            for (int i = 0; i < 12; i++)
            {
                result.Append(Alphabet[bytes[i] % Alphabet.Length]);
            }

            return result.ToString();

        }

        public static string GenerateUserId()
        {
            return $"usr_{GenerateShortUUID()}";
        }

        public static string GenerateRoleId()
        {
            return $"rol_{GenerateShortUUID()}";
        }

        public static bool IsValidUserId(string userId)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            
            if (id.Length != 12 || !id.StartsWith("usr_"))
            {
                return false;
            }

            var idPart = id[4 ..];
            return idPart.All(c => Alphabet.Contains(c));
        }
    }