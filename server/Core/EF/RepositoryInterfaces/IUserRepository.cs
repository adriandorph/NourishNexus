namespace server.Core.EF.RepositoryInterfaces;

public interface IUserRepository{
    //Create
    public Task<(Response, UserDTO)> CreateAsync(UserCreateDTO user);

    //Update
    public Task<Response> UpdateAsync(UserUpdateDTO user);

    //Delete
    public Task<Response> RemoveAsync(int id);
    
    //Read
    public Task<Option<UserDTO>> ReadByIDAsync(int userID);
    public Task<Option<UserNutritionDTO>> ReadWithNutritionByIDAsync(int userID);

}