using BackendLibrary.Entities;

namespace BackendLibrary.Responses.UserResponses
{
    public record UpdateUserResponse(bool Flag, string Message = null!, ApplicationUser NewUserUpdate = null!);
}
