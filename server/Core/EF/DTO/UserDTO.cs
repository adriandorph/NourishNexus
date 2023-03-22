namespace server.Core.EF.DTO;

public record UserDTO(
    int Id,
    string Nickname,
    [EmailAddress]
    string Email,
    List<int> SavedRecipeIds
);


public record UserCreateDTO {
    
    public string? Nickname {get; set;}
    
    [Required, EmailAddress]
    public string? Email {get; set;}
    
}

public record UserUpdateDTO : UserCreateDTO
{
    public int Id {get; set;}
    public List<int>? SavedRecipeIds {get; set;}
}
