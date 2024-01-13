namespace Domain.BoundedContext.Model.AggregatedRoot;

public sealed class Entity : IEntity
{
    public Guid Id { get; set; }
}