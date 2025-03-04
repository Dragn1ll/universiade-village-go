using Application.Utils;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface IShopsRepository
{
    Task<Result> CreateAsync(Shop shop, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<Shop>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result> ChangeNameAsync(Guid shopId, string newName, CancellationToken cancellationToken = default);
    Task<Result> ChangeRoomAsync(Guid shopId, int newRoom, CancellationToken cancellationToken = default);
    Task<Result> ChangeTelegramAsync(Guid shopId, int newTelegram, CancellationToken cancellationToken = default);
    Task<Result<bool>> CheckByIdAsync(Guid shopId, CancellationToken cancellationToken = default);
    Task<Result<bool>> CheckByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);
    Task<Result<Shop>> GetByIdAsync(Guid shopId, CancellationToken cancellationToken = default);
    Task<Result<Shop>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);
}