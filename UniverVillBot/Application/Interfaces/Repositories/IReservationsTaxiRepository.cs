using Application.Utils;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface IReservationsTaxiRepository
{
    Task<Result> CreateAsync(ReservationTaxi reservationTaxi, CancellationToken cancellationToken = default);
    Task<Result<ReservationTaxi>> GetByIdAsync(Guid reservationId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ReservationTaxi>>> GetByRequestIdAsync(Guid requestId,
        CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ReservationTaxi>>> GetByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid reservationId, CancellationToken cancellationToken = default);
}