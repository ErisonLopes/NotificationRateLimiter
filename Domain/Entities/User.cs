namespace Domain.Entities;

public class User
{
    public string UserId { get; set; }

    public User(string userId)
    {
        UserId = userId;
    }
}
