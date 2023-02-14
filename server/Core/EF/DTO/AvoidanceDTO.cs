namespace server.Core.EF;

public record AvoidanceDTO(
    int Id,
    string Keyword,
    int UserId
);

#nullable disable
public record AvoidanceCreateDTO
{
    public string Keyword {get; set;}
    
    public int UserId {get; set;}
}

public record AvoidanceUpdateDTO : AvoidanceCreateDTO
{
    public int Id {get; set;}
}