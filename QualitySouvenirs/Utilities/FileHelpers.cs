using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QualitySouvenirs.Utilities
{
    public class FileHelpers
    {
        public static bool ProcessImageFormFile(IFormFile formFile, ModelStateDictionary modelState)
        {
            var fileName = WebUtility.HtmlEncode(Path.GetFileName(formFile.FileName));

            var contentType = formFile.ContentType.ToLower();
            if (contentType != "image/bmp" 
                && contentType != "image/gif" 
                && contentType != "image/jpeg"
                && contentType != "image/png"
                && contentType != "image/svg+xml")
            {
                modelState.AddModelError(formFile.Name,
                    $"The {fileName} must be a image file.");
                return false;
            }

            if (formFile.Length == 0)
            {
                modelState.AddModelError(formFile.Name,
                    $"The {fileName} is empty.");
                return false;
            }

            if (formFile.Length > 10485760)
            {
                modelState.AddModelError(formFile.Name,
                    $"The {fileName} is exceeds 10 MB.");
                return false;
            }

            return true;
        }
    }
}
