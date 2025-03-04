using Application.Utils;
using Core.Models;

namespace Application.Interfaces.Repositories;

public interface IProductsRepository
{
    Task<Result> Create(Product product, CancellationToken cancellationToken = default);
    Task<Result<Product>> GetById(Guid productId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<Product>>> GetByShopId(Guid shopId, CancellationToken cancellationToken = default);
    Task<Result> UpdateAmount(Guid productId, uint amount, CancellationToken cancellationToken = default);
    Task<Result> UpdatePrice(Guid productId, uint newPrice, CancellationToken cancellationToken = default);
    Task<Result> Rename(Guid productId, string newName, CancellationToken cancellationToken = default);
    Task<Result> Delete(Guid productId, CancellationToken cancellationToken = default);
}