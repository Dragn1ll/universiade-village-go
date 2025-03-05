using Application.Interfaces.Repositories;
using Application.Utils;
using Core.Enums;
using Core.Models;
using MicroORM;

namespace Persistence.Repositories;

public class TaxiRequestsRepository(IMicroOrm microOrm) : ITaxiRequestsRepository
{
    private const string TableName = "TaxiRequests";
    
    public async Task<Result> Create(TaxiRequest taxiRequest, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.InsertAsync(taxiRequest, TableName, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result<IEnumerable<TaxiRequest>>> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<TaxiRequest>(TableName, null, 
                null, cancellationToken);
            
            return Result<IEnumerable<TaxiRequest>>.Success(result);
        }
        catch (Exception)
        {
            return Result<IEnumerable<TaxiRequest>>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result<IEnumerable<TaxiRequest>>> GetByUserId(Guid userId, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<TaxiRequest>(TableName,
                "UserId=@UserId", new { UserId = userId }, cancellationToken);
            
            return Result<IEnumerable<TaxiRequest>>.Success(result);
        }
        catch (Exception)
        {
            return Result<IEnumerable<TaxiRequest>>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result> Delete(Guid requestId, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.DeleteAsync(TableName, "Id=@RequestId", 
                new { RequestId = requestId}, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }
}