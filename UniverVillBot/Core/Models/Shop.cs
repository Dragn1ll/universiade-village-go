namespace Core.Models;

public class Shop
{
    public Guid Id { get; }
    public Guid OwnerId { get; }
    public string Name { get; }
    public int Room { get; }
    public bool IsOpen { get; }
    public string Telegram { get; }

    private Shop(Guid id, Guid ownerId, string name, int room, bool isOpen, string telegram)
    {
        Id = id;
        OwnerId = ownerId;
        Name = name;
        Room = room;
        IsOpen = isOpen;
        Telegram = telegram;
    }

    public static Shop Create(Guid ownerId, string name, int room, bool isOpen, string telegram, Guid? id = null)
    {
        if (ownerId == Guid.Empty) 
            throw new ArgumentNullException(nameof(ownerId), "Owner details not specified");
        
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Shop name not specified");
        
        if (room < 101 || room > 520 || room % 100 > 20)
            throw new ArgumentOutOfRangeException(nameof(room), "Room number must be between 101 and 520");
        
        if (string.IsNullOrWhiteSpace(telegram))
            throw new ArgumentNullException(nameof(telegram), "Shop telegram not specified");
        
        return new Shop(id ?? Guid.NewGuid(), ownerId, name, room, isOpen, telegram);
    }
}