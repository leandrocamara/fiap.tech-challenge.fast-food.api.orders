using SharedKernel.Domain;

namespace BoundedContext.Domain.Model.AggregatedRoot;

public sealed class Entity : IEntity
{
    public Guid Id { get; set; }
}