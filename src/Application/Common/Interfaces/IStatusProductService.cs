namespace Tekton.Application.Common.Interfaces;
public interface IStatusProductService
{
    Task<Dictionary<int, string>> GetProductStatus();
}
