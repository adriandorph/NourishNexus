namespace server.Core.Model;

public record AuthorDTO(
    [Required] string Id,
    [Required] string Name,
    string? Picture
);