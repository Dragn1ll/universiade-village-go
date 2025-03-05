namespace Core.Models;

public class ReservationTaxi
{
    public Guid Id { get; }
    public Guid RequestId { get; }
    public Guid UserId { get; }
    public uint SeatsAmount { get; }

    public ReservationTaxi()
    {
        Id = Guid.Empty;
        RequestId = Guid.Empty;
        UserId = Guid.Empty;
        SeatsAmount = 0;
    }

    private ReservationTaxi(Guid id, Guid requestId, Guid userId, uint seatsAmount)
    {
        Id = id;
        RequestId = requestId;
        UserId = userId;
        SeatsAmount = seatsAmount;
    }

    public static ReservationTaxi Create(Guid requestId, Guid userId, uint seatsAmount, Guid? id = null)
    {
        if (requestId == Guid.Empty)
            throw new ArgumentNullException(nameof(requestId), $"{nameof(requestId)} cannot be empty");
        
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId), $"{nameof(userId)} cannot be empty");
        
        if (seatsAmount is 0 or > 3)
            throw new ArgumentNullException(nameof(seatsAmount), 
                $"{nameof(seatsAmount)} cannot be zero and less than 3");
        
        return new ReservationTaxi(id ?? Guid.NewGuid(), requestId, userId, seatsAmount);
    }
}