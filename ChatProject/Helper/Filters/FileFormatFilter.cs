using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Chat.Application.Helper.Extentions;
using System.Collections;

namespace Chat.Api.Helper.Filters
{
    public class FileFormatFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            long maxFileSize = 10 * 1024 * 1024;
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var file = context.HttpContext.Request.Form.Files.FirstOrDefault();

            if (file != null)
            {
                if (file.Length > maxFileSize)
                {
                    context.Result = new BadRequestObjectResult("File size exceeds the maximum limit of 10 MB.");
                    return;
                }

                var extension = Path.GetExtension(file.FileName);
                if (!validExtensions.Contains(extension.ToLower()))
                {
                    context.Result = new BadRequestObjectResult("Required file format is jpeg, jpg, png, gif !!");
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("No file was uploaded.");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
