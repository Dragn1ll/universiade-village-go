using Application.Utils;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface IReservationTaxiRepository
{
    Task<Result> CreateAsync(ReservationTaxi reservationTaxi, CancellationToken cancellationToken);
    Task<Result<ReservationTaxi>> GetByIdAsync(Guid reservationId, CancellationToken cancellationToken);
    Task<IEnumerable<ReservationTaxi>> GetByRequestIdAsync(Guid requestId, CancellationToken cancellationToken);
    Task<IEnumerable<ReservationTaxi>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(Guid reservationId, CancellationToken cancellationToken);
}