using Application.Utils;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface ITaxiRequestRepository
{
    Task<Result> Create(TaxiRequest taxiRequest, CancellationToken cancellationToken);
    Task<Result<IEnumerable<TaxiRequest>>> GetAll(CancellationToken cancellationToken);
    Task<Result<IEnumerable<TaxiRequest>>> GetByUserId(Guid userId, CancellationToken cancellationToken);
    Task<Result> Delete(Guid requestId, CancellationToken cancellationToken);
}