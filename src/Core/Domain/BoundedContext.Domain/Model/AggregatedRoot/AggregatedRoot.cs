using SharedKernel.Domain;

namespace BoundedContext.Domain.Model.AggregatedRoot;

public sealed class AggregatedRoot : IAggregatedRoot
{
    public Guid Id { get; set; }
}