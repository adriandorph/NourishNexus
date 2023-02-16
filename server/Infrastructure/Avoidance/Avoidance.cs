namespace server.Infrastructure;

public class Avoidance {
    
    public int Id {get; set;}
    public string Keyword {get; set;}
    public User User {get; set;}

    public Avoidance(string keyword, User user)
    {
        this.Keyword = keyword;
        this.User = user;
    }

#nullable disable
    public Avoidance() {}

    public AvoidanceDTO ToDTO()
        => new AvoidanceDTO(Id, Keyword, User.Id);
}