using Application.Utils;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface IShopsRepository
{
    Task<Result> CreateAsync(Shop shop, CancellationToken cancellationToken);
    Task<IEnumerable<Shop>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result> ChangeNameAsync(Guid shopId, string newName, CancellationToken cancellationToken);
    Task<Result> ChangeRoomAsync(Guid shopId, int newRoom, CancellationToken cancellationToken);
    Task<Result> ChangeTelegramAsync(Guid shopId, int newTelegram, CancellationToken cancellationToken);
    Task<Result<bool>> CheckByIdAsync(Guid shopId, CancellationToken cancellationToken);
    Task<Result<bool>> CheckByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken);
    Task<Result<Shop>> GetByIdAsync(Guid shopId, CancellationToken cancellationToken);
    Task<Result<Shop>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken);
}