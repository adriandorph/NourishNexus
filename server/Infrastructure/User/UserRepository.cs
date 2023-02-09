namespace server.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly NourishNexusContext _context;

    public UserRepository(NourishNexusContext context)
    {
        _context = context;
    }
}