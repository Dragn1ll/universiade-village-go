namespace Core.Models;

public class Product
{
    public Guid Id { get; }
    public Guid ShopId { get; }
    public string Name { get; }
    public int Amount { get; }

    private Product(Guid id, Guid shopId, string name, int amount)
    {
        Id = id;
        ShopId = shopId;
        Name = name;
        Amount = amount;
    }

    public static Product Create(Guid shopId, string name, int amount, Guid? id = null)
    {
        if (shopId == Guid.Empty)
            throw new ArgumentNullException(nameof(shopId), $"{nameof(shopId)} cannot be null.");
        
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), $"{nameof(name)} cannot be null or whitespace.");
        
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), $"{nameof(amount)} cannot be negative.");
        
        return new Product(id ?? Guid.NewGuid(), shopId, name, amount);
    }
}