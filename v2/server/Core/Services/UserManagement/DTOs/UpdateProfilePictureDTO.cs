namespace server.Core.Services.UserManagement.DTOs;

public record UpdateProfilePictureDTO(
    string UserId,
    string ImageBase64
);