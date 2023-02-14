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
}