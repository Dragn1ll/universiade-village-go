using Application.Interfaces.Repositories;
using Application.Utils;
using Core.Enums;
using Core.Models;
using MicroORM;

namespace Persistence.Repositories;

public class ReservationsProductsRepository(IMicroOrm microOrm) : IReservationsProductsRepository
{
    private const string TableName = "ReservationsProducts";
    
    public async Task<Result> CreateAsync(ReservationProduct reservationProduct, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.InsertAsync(reservationProduct, TableName, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result<ReservationProduct>> GetByIdAsync(Guid reservationId, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<ReservationProduct>(TableName, 
                "Id=@ReservationId", new { ReservationId = reservationId }, cancellationToken);

            return Result<ReservationProduct>.Success(result.First());
        }
        catch (Exception)
        {
            return Result<ReservationProduct>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result<IEnumerable<ReservationProduct>>> GetByShopIdAsync(Guid shopId, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<ReservationProduct>(TableName, 
                "ShopId=@ShopId", new { ShopId = shopId }, cancellationToken);
            
            return Result<IEnumerable<ReservationProduct>>.Success(result);
        }
        catch (Exception)
        {
            return Result<IEnumerable<ReservationProduct>>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result<IEnumerable<ReservationProduct>>> GetByProductIdAsync(Guid productId, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<ReservationProduct>(TableName, 
                "ProductId=@ProductId", new { ProductId = productId }, cancellationToken);
            
            return Result<IEnumerable<ReservationProduct>>.Success(result);
        }
        catch (Exception)
        {
            return Result<IEnumerable<ReservationProduct>>.Failure(new Error(ErrorType.ServerError, 
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