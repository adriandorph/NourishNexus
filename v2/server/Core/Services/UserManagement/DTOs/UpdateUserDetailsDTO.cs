namespace server.Core.Services.UserManagement.DTOs
{
    public record UpdateUserDetailsDTO(
        string UserId,
        string? Email,
        string? Nickname,
        string? Bio
    );
}