using Application.Utils;
using Core.Enums;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<Result> CreateAsync(User user, CancellationToken cancellationToken);
    Task<Result<bool>> CheckById(Guid userId, CancellationToken cancellationToken);
    Task<Result<bool>> CheckByTelegram(Guid userId, CancellationToken cancellationToken);
    Task<Result> ChangeRoom(Guid userId, int newRoom, CancellationToken cancellationToken);
    Task<Result> ChangeName(Guid userId, string newName, CancellationToken cancellationToken);
    Task<Result<Role>> GetRoleById(Guid userId, CancellationToken cancellationToken);
}