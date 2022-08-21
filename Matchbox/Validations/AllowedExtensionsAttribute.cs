using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Matchbox.Validations
{
    /// <summary>
    /// Determina la/las extensiones válidas del archivo.
    /// </summary>
    /// <remarks>
    /// Ej.: [AllowedExtensions(new string[] { ".jpeg", ".png" })]
    /// </remarks>
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            var file = value as IFormFile;
            var extension = Path.GetExtension(file.FileName);

            if (!_extensions.Contains(extension.ToLower()))
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        protected string GetErrorMessage()
        {
            return $"Formato de archivo no válido. Solo se admite: {string.Join(", ", _extensions)}";
        }
    }
}