namespace server.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly NourishNexusContext _context;

    public UserRepository(NourishNexusContext context)
    {
        _context = context;
    }

    public async Task<(Response, UserDTO)> CreateAsync(UserCreateDTO user)
    {
        //Conflict?
        var conflict = await _context.Users
            .Where(u => u.Email == user.Email)
            .FirstOrDefaultAsync();
            
        if (conflict != null) return (Response.Conflict, new UserDTO(-1, user.Email, user.Nickname));
        

        //Create entity and insert it into the database context
        var entity = new User
        (
            user.Nickname,
            user.Email
        );

        await _context.Users.AddAsync(entity);

        await _context.SaveChangesAsync();

        return (Response.Created, new UserDTO(entity.Id, entity.Email, entity.Nickname));

    }

    public async Task<Response> UpdateAsync(UserUpdateDTO user)//Returns NotFound, Conflict or Updated
    {
        var userEntity = await _context.Users
            .Where(u => u.Id == user.Id)
            .FirstOrDefaultAsync();
        
        
        if(userEntity == null) return Response.NotFound;

        if(_context.Users.Any(u => u.Id != user.Id && u.Email == user.Email)) return Response.Conflict;

        if (userEntity.Email != null && !userEntity.Email.Equals(user.Email) && user.Email != null)
        {
            userEntity.Email = user.Email;
        }

        if (userEntity.Nickname != null && !userEntity.Nickname.Equals(user.Nickname) && user.Nickname != null)
        {
            userEntity.Nickname = user.Nickname;
        }

        await _context.SaveChangesAsync();
        
        return Response.Updated;
    }

    public async Task<Response> RemoveAsync(int id)
    {
        var userEntity = await _context.Users
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        
        if(userEntity == null) 
        {
            return Response.NotFound;
        }

        _context.Users.Remove(userEntity);
        await _context.SaveChangesAsync();
        
        return Response.Deleted;
    }

    public async Task<Option<UserDTO>> ReadByIDAsync(int Id)
    {
        var users = from u in _context.Users
                        where u.Id == Id
                        select new UserDTO(u.Id, u.Email, u.Nickname);

        return await users.FirstOrDefaultAsync();
    }
}