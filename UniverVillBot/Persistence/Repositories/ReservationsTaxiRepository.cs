using Application.Interfaces.Repositories;
using Application.Utils;
using Core.Enums;
using Core.Models;
using MicroORM;

namespace Persistence.Repositories;

public class ReservationsTaxiRepository(IMicroOrm microOrm) : IReservationsTaxiRepository
{
    private const string TableName = "ReservationsTaxi";
    
    public async Task<Result> CreateAsync(ReservationTaxi reservationTaxi, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.InsertAsync(reservationTaxi, TableName, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result<ReservationTaxi>> GetByIdAsync(Guid reservationId, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<ReservationTaxi>(TableName, 
                "Id=@ReservationId", new { ReservationId = reservationId }, cancellationToken);

            return Result<ReservationTaxi>.Success(result.First());
        }
        catch (Exception)
        {
            return Result<ReservationTaxi>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result<IEnumerable<ReservationTaxi>>> GetByRequestIdAsync(Guid requestId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<ReservationTaxi>(TableName, 
                "RequestId=@RequestId", new { RequestId = requestId }, cancellationToken);
            
            return Result<IEnumerable<ReservationTaxi>>.Success(result);
        }
        catch (Exception)
        {
            return Result<IEnumerable<ReservationTaxi>>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result<IEnumerable<ReservationTaxi>>> GetByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<ReservationTaxi>(TableName, 
                "UserId=@UserId", new { UserId = userId }, cancellationToken);
            
            return Result<IEnumerable<ReservationTaxi>>.Success(result);
        }
        catch (Exception)
        {
            return Result<IEnumerable<ReservationTaxi>>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result> DeleteAsync(Guid reservationId, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.DeleteAsync(TableName, "Id=@ReservationId", 
                new { ReservationId = reservationId }, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }
}