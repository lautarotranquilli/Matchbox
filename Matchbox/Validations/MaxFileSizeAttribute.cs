using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Matchbox.Validations
{
    /// <summary>
    /// Determina el tamaño máximo del archivo (bytes).
    /// </summary>
    /// <remarks>
    /// Ej.: [MaxFileSize(5* 1024 * 1024)] => 5mb
    /// </remarks>
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var file = value as IFormFile;

            if (file.Length > _maxFileSize)
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        protected string GetErrorMessage()
        {
            if (_maxFileSize < 1048576)
                return $"El archivo no debe superar los {_maxFileSize}Bytes.";

            return $"El archivo no debe superar los {_maxFileSize / 1048576}MB.";
        }
    }
}