using Application.Utils;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface IReservationsProductsRepository
{
    Task<Result> CreateAsync(ReservationProduct reservationProduct, CancellationToken cancellationToken = default);
    Task<Result<ReservationProduct>> GetByIdAsync(Guid reservationId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ReservationProduct>>> GetByShopIdAsync(Guid shopId, 
        CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ReservationProduct>>> GetByProductIdAsync(Guid productId, 
        CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid reservationId, CancellationToken cancellationToken = default);
}