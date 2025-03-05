using Application.Interfaces.Repositories;
using Application.Utils;
using Core.Enums;
using Core.Models;
using MicroORM;

namespace Persistence.Repositories;

public class UsersRepository(IMicroOrm microOrm) : IUsersRepository
{
    private const string TableName = "Users";
    
    public async Task<Result> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.InsertAsync(user, TableName, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result<bool>> CheckById(Guid userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<User>(TableName, "Id=@UserId",
                new {UserId = userId}, cancellationToken);

            return Result<bool>.Success(result.Any());
        }
        catch (Exception)
        {
            return Result<bool>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result<bool>> CheckByTelegramAsync(string telegram, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<User>(TableName, "Telegram=@Telegram",
                new {Telegram = telegram}, cancellationToken);

            return Result<bool>.Success(result.Any());
        }
        catch (Exception)
        {
            return Result<bool>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }    }

    public async Task<Result> ChangeRoomAsync(Guid userId, int newRoom, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.UpdateAsync(TableName, "Id=@UserId", 
                new {UserId = userId, Room = newRoom}, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result> ChangeNameAsync(Guid userId, string newName, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.UpdateAsync(TableName, "Id=@UserId", 
                new {UserId = userId, Name = newName}, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }

    }

    public async Task<Result<Role>> GetRoleByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<User>(TableName, "Id=@UserId",
                new { UserId = userId }, cancellationToken);

            return Result<Role>.Success(result.First().Role);
        }
        catch (Exception)
        {
            return Result<Role>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }
}