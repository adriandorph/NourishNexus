namespace server.Infrastructure;

public class User
{
    public int Id {get;set;}

    [StringLength(25)]
    public string Nickname {get;set;}

    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Email {get;set;}

    public List<Recipe> SavedRecipes {get; set;}

    public User(string Nickname, string Email, List<Recipe> savedRecipes) {
        this.Nickname = Nickname;
        this.Email = Email;
        this.SavedRecipes = savedRecipes;
    }

    #nullable disable
    public User() {}

    public UserDTO ToDTO()
            => new UserDTO(
                this.Id,   
                this.Nickname,
                this.Email,
                this.SavedRecipes.Select(r => r.Id).ToList()
            );

}