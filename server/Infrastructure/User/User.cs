namespace server.Infrastructure;

public class User 
{
    public int Id {get;set;}

    [StringLength(25)]
    public string? Nickname {get;set;}

    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Email {get;set;}

    public User(string nickname, string email) {
        this.Nickname = nickname;
        this.Email = email;
    }
}