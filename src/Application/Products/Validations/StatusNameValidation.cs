using Tekton.Application.Common.Models;

namespace Tekton.Application.Products.Validations;
public static class StatusNameValidation
{
    public static bool ValidStatusName(string statusName)
    {
        try
        {
            return StatusDictionary.GetStatus().Any(x => string.Equals(x.Value, statusName, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception)
        {
            return false;
        }
    }
}
