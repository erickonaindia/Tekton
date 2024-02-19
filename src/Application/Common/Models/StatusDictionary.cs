namespace Tekton.Application.Common.Models;
public static class StatusDictionary
{
    public static Dictionary<int, string> GetStatus()
    {
        return new Dictionary<int, string>
        {
            { 1, "Active" },
            { 0, "Inactive" }
        };
    }
}
