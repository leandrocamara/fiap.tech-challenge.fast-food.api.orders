using BoundedContext.Domain.Model.AggregatedRoot;

namespace Persistence.Repositories;

public class AggregatedRootRepository : IAggregatedRootRepository
{
    public Task Save(AggregatedRoot entity)
    {
        throw new NotImplementedException();
    }

    public Task<AggregatedRoot> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}