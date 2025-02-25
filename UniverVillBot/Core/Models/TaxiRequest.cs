namespace Core.Models;

public class TaxiRequest
{
    public Guid Id { get; }
    public Guid OwnerId { get; }
    public DateTime DateTime { get; }
    public string Origin { get; }
    public string Destination { get; }
    public int PassengerAmount { get; }

    private TaxiRequest(Guid id, Guid ownerId, DateTime dateTime, string origin, string destination,
        int passengerAmount)
    {
        Id = id;
        OwnerId = ownerId;
        DateTime = dateTime;
        Origin = origin;
        Destination = destination;
        PassengerAmount = passengerAmount;
    }

    public static TaxiRequest Create(Guid ownerId, DateTime dateTime, string origin, string destination,
        int passengerAmount, Guid? id = null)
    {
        if (ownerId == Guid.Empty)
            throw new ArgumentNullException(nameof(ownerId), $"{nameof(ownerId)} cannot be null.");
        
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime), $"{nameof(dateTime)} cannot be null.");
        
        if (string.IsNullOrWhiteSpace(origin))
            throw new ArgumentNullException(nameof(origin), $"{nameof(origin)} cannot be null.");
        
        if (string.IsNullOrWhiteSpace(destination))
            throw new ArgumentNullException(nameof(destination), $"{nameof(destination)} cannot be null.");
        
        if (passengerAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(passengerAmount), 
                $"{nameof(passengerAmount)} cannot be less than zero.");
        
        return new TaxiRequest(id ?? Guid.NewGuid(), ownerId, dateTime, origin, destination, passengerAmount);
    }
}