using Tekton.Domain.Entities;

namespace Tekton.Domain.Events.Products;

public class ProductCompletedEvent : BaseEvent
{
    public ProductCompletedEvent(Product product)
    {
        Product = product;
    }
    public Product Product { get; }
}
