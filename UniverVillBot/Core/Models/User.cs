using Core.Enums;

namespace Core.Models;

public class User
{
    public Guid Id { get; }
    public Role Role { get; }
    public string Name { get; }
    public int Room { get; }
    public string Telegram { get; }

    private User(Guid id, Role role, string name, int room, string telegram)
    {
        Id = id;
        Role = role;
        Name = name;
        Room = room;
        Telegram = telegram;
    }

    public static User Create(string name, int room, string telegram, Guid? id = null, Role role = Role.User)
    {
        if (string.IsNullOrWhiteSpace(telegram))
            throw new ArgumentNullException(nameof(telegram), "Telegram text cannot be null or empty");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Name cannot be null or empty");
        
        if (room < 101 || room > 520 || room % 100 > 20)
            throw new ArgumentOutOfRangeException(nameof(room), "Room must be between 101 and 520");
        
        return new User(id ?? Guid.NewGuid(), role, name, room, telegram);
    }
}