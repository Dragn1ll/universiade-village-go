using Application.Utils;
using Core.Enums;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<Result> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<Result<bool>> CheckById(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<bool>> CheckByTelegramAsync(string telegram, CancellationToken cancellationToken = default);
    Task<Result> ChangeRoomAsync(Guid userId, int newRoom, CancellationToken cancellationToken = default);
    Task<Result> ChangeNameAsync(Guid userId, string newName, CancellationToken cancellationToken = default);
    Task<Result<Role>> GetRoleByIdAsync(Guid userId, CancellationToken cancellationToken = default);
}