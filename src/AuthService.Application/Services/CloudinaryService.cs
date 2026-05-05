using AuthService.Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace AuthService.Application.Services;

public class CloudinaryService(IConfiguration configuration) : ICloudinaryService
{
    private readonly Cloudinary _cloudinary = new(new Account(
        configuration["CloudinarySettings:CloudName"],
        configuration["CloudinarySettings:ApiKey"],
        configuration["CloudinarySettings:ApiSecret"]
    ));

    private readonly string _folder = configuration["CloudinarySettings:Folder"] ?? "auth_service/profiles";
    private readonly string _baseUrl = configuration["CloudinarySettings:BaseUrl"] ?? "https://res.cloudinary.com/dktwa0obs/image/upload/v1/auth_dotnet/";

    public async Task<string> UploadImageAsync(IFileData imageFile, string fileName)
    {
        try
        {
            using var stream = new MemoryStream(imageFile.Data);

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(imageFile.FileName, stream),
                PublicId = $"{_folder}/{SanitizeFileName(fileName)}",
                Folder = _folder,
                Transformation = new Transformation()
                    .Width(400)
                    .Height(400)
                    .Crop("fill")
                    .Gravity("face")
                    .Quality("auto")
                    .FetchFormat("auto")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new InvalidOperationException($"Error uploading image: {uploadResult.Error.Message}");
            }

            return SanitizeFileName(fileName);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to upload image to Cloudinary: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteImageAsync(string publicId)
    {
        try
        {
            var deleteParams = new DelResParams
            {
                PublicIds = new List<string> { publicId }
            };

            var result = await _cloudinary.DeleteResourcesAsync(deleteParams);
            return result.Deleted?.ContainsKey(publicId) == true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Devuelve la URL completa de la imagen en Cloudinary si existe.
    /// Retorna string.Empty si imagePath es null o vacío (frontend maneja fallback).
    /// </summary>
    public string GetFullImageUrl(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            return string.Empty;

        var pathToUse = imagePath.Contains('/') ? imagePath : $"{_folder}/{imagePath}";
        return $"{_baseUrl}{pathToUse}";
    }

    /// <summary>
    /// Método obligatorio de la interfaz ICloudinaryService.
    /// Devuelve string.Empty porque el backend ya no maneja default avatars.
    /// </summary>
    public string GetDefaultAvatarUrl()
    {
        return string.Empty;
    }

    private static string SanitizeFileName(string fileName)
    {
        return fileName
            .Trim()
            .Replace(" ", "_")
            .Replace("-", "_")
            .ToLowerInvariant();
    }
}