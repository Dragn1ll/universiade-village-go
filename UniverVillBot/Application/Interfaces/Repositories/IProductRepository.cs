using Application.Utils;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Result> Create(Product product, CancellationToken cancellationToken);
    Task<Result<Product>> GetById(Guid productId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<Product>>> GetByShopId(Guid shopId, CancellationToken cancellationToken);
    Task<Result> UpdateAmount(Guid productId, uint amount, CancellationToken cancellationToken);
    Task<Result> UpdatePrice(Guid productId, uint newPrice, CancellationToken cancellationToken);
    Task<Result> Rename(Guid productId, string newName, CancellationToken cancellationToken);
    Task<Result> Delete(Guid productId, CancellationToken cancellationToken);
}