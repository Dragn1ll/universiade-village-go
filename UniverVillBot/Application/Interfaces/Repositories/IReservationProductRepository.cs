using Application.Utils;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface IReservationProductRepository
{
    Task<Result> CreateAsync(ReservationProduct reservationProduct, CancellationToken cancellationToken);
    Task<Result<ReservationProduct>> GetByIdAsync(Guid reservationId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<ReservationProduct>>> GetByShopIdAsync(Guid shopId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<ReservationProduct>>> GetByProductIdAsync(Guid productId, 
        CancellationToken cancellationToken);
    Task<Result> DeleteAsync(Guid reservationId, CancellationToken cancellationToken);
}