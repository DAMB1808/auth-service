/*
using System.Security.Cryptography; // Para generar números aleatorios criptográficamente seguros
using System.Text;
namespace AuthService.Application.Services;


public static class UuidGenerator
{
    private const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";


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
        return 
        result.ToString();
    }
    public static string GenerateUserId()
    {
        return $"usr_{GenerateShortUUID()}";
    }

    public static string GenerateRoleId()
    {
        return $"rol_{GenerateShortUUID()}";
    }

    public static bool IsValidUserId(string id)
    {
      if(string.IsNullOrEmpty(userId))
      {
        return false;
      }

    if (id.Legth != 16 || !id.StartsWith("usr_"))
    {
        return false;
    }

    var idPart = id[4..];
    return idPart.All(c => Alphabet.Contains(c));
    }



}
*/

using System.Security.Cryptography; // Para generar números aleatorios criptográficamente seguros
using System.Text; // Para usar StringBuilder

namespace AuthService.Application.Services;

public static class UuidGenerator
{
    // Conjunto de caracteres permitidos (62 caracteres: 0-9, A-Z, a-z)
    private const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    // Genera un identificador aleatorio corto de 12 caracteres
    public static string GenerateShortUUID()
    {
        // Crea un generador de números aleatorios seguro (criptográfico)
        using var rng = RandomNumberGenerator.Create();

        // Arreglo de 12 bytes (cada byte será convertido en un carácter)
        var bytes = new byte[12];

        // Llena el arreglo con valores aleatorios
        rng.GetBytes(bytes);

        // StringBuilder para construir el resultado final
        var result = new StringBuilder(12);

        // Recorre los 12 bytes generados
        for (int i = 0; i < 12; i++)
        {
            // Usa el valor del byte como índice (usando módulo 62)
            // para seleccionar un carácter del alfabeto
            result.Append(Alphabet[bytes[i] % Alphabet.Length]);
        }

        // Devuelve la cadena generada de 12 caracteres
        return result.ToString();
    }

    // Genera un ID de usuario con prefijo "usr_"
    public static string GenerateUserId()
    {
        return $"usr_{GenerateShortUUID()}";
    }

    // Genera un ID de rol con prefijo "rol_"
    public static string GenerateRoleId()
    {
        return $"rol_{GenerateShortUUID()}";
    }

    // Valida si un string es un UserId válido
    public static bool IsValidUserId(string id)
    {
        // ⚠ ERROR EN TU CÓDIGO ORIGINAL:
        // Usabas "userId" pero el parámetro se llama "id"

        // Si es null o vacío, no es válido
        if (string.IsNullOrEmpty(id))
        {
            return false;
        }

        // ⚠ ERROR EN TU CÓDIGO ORIGINAL:
        // "Legth" está mal escrito, debe ser "Length"

        // Debe tener exactamente 16 caracteres:
        // "usr_" (4 caracteres) + 12 del UUID corto
        if (id.Length != 16 || !id.StartsWith("usr_"))
        {
            return false;
        }

        // Extrae la parte después del prefijo "usr_"
        var idPart = id[4..];

        // Verifica que todos los caracteres pertenezcan al alfabeto permitido
        return idPart.All(c => Alphabet.Contains(c));
    }
}
