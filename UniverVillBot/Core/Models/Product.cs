namespace Core.Models;

public class Product
{
    public Guid Id { get; }
    public Guid ShopId { get; }
    public string Name { get; }
    public uint Amount { get; }
    public uint Price { get; }

    private Product(Guid id, Guid shopId, string name, uint amount, uint price)
    {
        Id = id;
        ShopId = shopId;
        Name = name;
        Amount = amount;
        Price = price;
    }

    public static Product Create(Guid shopId, string name, uint amount, uint price, Guid? id = null)
    {
        if (shopId == Guid.Empty)
            throw new ArgumentNullException(nameof(shopId), $"{nameof(shopId)} cannot be null.");
        
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), $"{nameof(name)} cannot be null or whitespace.");
        
        return new Product(id ?? Guid.NewGuid(), shopId, name, amount, price);
    }
}