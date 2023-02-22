namespace server.Core.EF.DTO;

public record UserDTO(
    int Id,
    
    [EmailAddress]
    string Email,

    string Nickname,
    List<int> SavedRecipeIds
);


public record UserCreateDTO {
    
    [EmailAddress]
    public string? Email {get; set;}
    public string? Nickname {get; set;}
}

public record UserUpdateDTO : UserCreateDTO
{
    public int Id {get; set;}
    public List<int>? SavedRecipeIds {get; set;}
}
