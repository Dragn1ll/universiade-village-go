using Application.Utils;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface ITaxiRequestsRepository
{
    Task<Result> Create(TaxiRequest taxiRequest, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<TaxiRequest>>> GetAll(CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<TaxiRequest>>> GetByUserId(Guid userId, CancellationToken cancellationToken = default);
    Task<Result> Delete(Guid requestId, CancellationToken cancellationToken = default);
}