using Application.Interfaces.Repositories;
using Application.Utils;
using Core.Enums;
using Core.Models;
using MicroORM;

namespace Persistence.Repositories;

public class ShopsRepository(IMicroOrm microOrm) : IShopsRepository
{
    private const string TableName = "Shops";
    
    public async Task<Result> CreateAsync(Shop shop, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.InsertAsync(shop, TableName, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result<IEnumerable<Shop>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<Shop>(TableName, 
                cancellationToken: cancellationToken);
            
            return Result<IEnumerable<Shop>>.Success(result);
        }
        catch (Exception)
        {
            return Result<IEnumerable<Shop>>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result> ChangeNameAsync(Guid shopId, string newName, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.UpdateAsync(TableName,  "Id=@ShopId", 
                new{ShopId = shopId, Name = newName}, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result> ChangeRoomAsync(Guid shopId, int newRoom, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.UpdateAsync(TableName,  "Id=@ShopId", 
                new{ShopId = shopId, Room = newRoom}, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result> ChangeTelegramAsync(Guid shopId, int newTelegram, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.UpdateAsync(TableName,  "Id=@ShopId", 
                new{ShopId = shopId, Telegram = newTelegram}, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result<bool>> CheckByIdAsync(Guid shopId, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<Shop>(TableName, "Id=@ShopId", 
                new{ShopId = shopId}, cancellationToken);
            
            return Result<bool>.Success(result.Any());
        }
        catch (Exception)
        {
            return Result<bool>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result<bool>> CheckByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<Shop>(TableName, "OwnerId=@OwnerId", 
                new{OwnerId = ownerId}, cancellationToken);
            
            return Result<bool>.Success(result.Any());
        }
        catch (Exception)
        {
            return Result<bool>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result<Shop>> GetByIdAsync(Guid shopId, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<Shop>(TableName, "Id=@ShopId", 
                new{ShopId = shopId}, cancellationToken);
            
            return Result<Shop>.Success(result.First());
        }
        catch (Exception)
        {
            return Result<Shop>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result<Shop>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<Shop>(TableName, "OwnerId=@OwnerId", 
                new{OwnerId = ownerId}, cancellationToken);
            
            return Result<Shop>.Success(result.First());
        }
        catch (Exception)
        {
            return Result<Shop>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }
}