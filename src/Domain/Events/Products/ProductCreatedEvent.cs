using Tekton.Domain.Entities;

namespace Tekton.Domain.Events;

public class ProductCreatedEvent : BaseEvent
{
    public ProductCreatedEvent(Product product)
    {
        Product = product;
    }
    public Product Product { get; }
}
