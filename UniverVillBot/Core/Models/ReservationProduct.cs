namespace Core.Models;

public class ReservationProduct
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid ProductId { get; }
    public uint Amount { get; }
    public DateTime DateTime { get; }

    public ReservationProduct()
    {
        Id = Guid.Empty;
        UserId = Guid.Empty;
        ProductId = Guid.Empty;
        Amount = 0;
        DateTime = DateTime.MinValue;
    }

    private ReservationProduct(Guid id, Guid userId, Guid productId, uint amount, DateTime dateTime)
    {
        Id = id;
        UserId = userId;
        ProductId = productId;
        Amount = amount;
        DateTime = dateTime;
    }

    public static ReservationProduct Create(Guid userId, Guid productId, uint amount, DateTime dateTime, Guid? id = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId), $"{nameof(userId)} cannot be null.");
        
        if (productId == Guid.Empty)
            throw new ArgumentNullException(nameof(productId), $"{nameof(productId)} cannot be null.");
        
        if (amount < 1)
            throw new ArgumentOutOfRangeException(nameof(amount), $"{nameof(amount)} cannot be less than 1.");
        
        return new ReservationProduct(id ?? Guid.NewGuid(), userId, productId, amount, dateTime);
    }
}