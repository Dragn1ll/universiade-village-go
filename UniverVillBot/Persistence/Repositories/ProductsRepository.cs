using Application.Interfaces.Repositories;
using Application.Utils;
using Core.Enums;
using Core.Models;
using MicroORM;

namespace Persistence.Repositories;

public class ProductsRepository(IMicroOrm microOrm) : IProductsRepository
{
    private const string TableName = "Products";
    
    public async Task<Result> Create(Product product, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.InsertAsync(product, TableName, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result<Product>> GetById(Guid productId, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<Product>(TableName, 
                "ProductId=@ProductId", new { ProductId = productId }, cancellationToken);
            
            return Result<Product>.Success(result.First());
        }
        catch (Exception)
        {
            return Result<Product>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result<IEnumerable<Product>>> GetByShopId(Guid shopId, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await microOrm.SelectAsync<Product>(TableName, "ShopId=@ShopId", 
                new { ShopId = shopId }, cancellationToken);
            
            return Result<IEnumerable<Product>>.Success(result);
        }
        catch (Exception)
        {
            return Result<IEnumerable<Product>>.Failure(new Error(ErrorType.ServerError, 
                "Database error, try again later."));
        }
    }

    public async Task<Result> UpdateAmount(Guid productId, uint amount, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.UpdateAsync(TableName, "ProductId=@ProductId", 
                new { ProductId = productId, Amount = amount}, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result> UpdatePrice(Guid productId, uint newPrice, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.UpdateAsync(TableName, "ProductId=@ProductId", 
                new { ProductId = productId, Price = newPrice }, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result> Rename(Guid productId, string newName, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.UpdateAsync(TableName, "ProductId=@ProductId", 
                new { ProductId = productId, Name = newName }, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }

    public async Task<Result> Delete(Guid productId, CancellationToken cancellationToken = default)
    {
        try
        {
            await microOrm.DeleteAsync<Product>(TableName, "ProductId=@ProductId", 
                new { ProductId = productId }, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error(ErrorType.ServerError, "Database error, try again later."));
        }
    }
}