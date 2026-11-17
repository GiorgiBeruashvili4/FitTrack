using FitTrack.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Extensions;

public static class ValidationResponseExtension
{
    public static IActionResult Create(ActionContext context)
    {
        var errors = context.ModelState
            .Where(e => e.Value != null && e.Value.Errors.Count > 0)
            .ToDictionary(
                e => ToCamelCase(e.Key),
                e => e.Value!.Errors.Select(x => x.ErrorMessage).ToArray());

        var body = new ErrorResponse
        {
            Status = 400,
            Message = "Validation failed",
            Errors = errors
        };

        return new BadRequestObjectResult(body);
    }

    public static string ToCamelCase(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return key;
        }

        return char.ToLowerInvariant(key[0]) + key.Substring(1);
    }
}
