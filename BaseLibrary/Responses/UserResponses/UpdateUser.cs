using BaseLibrary.Entities;

namespace BaseLibrary.Responses.UserResponses
{
    public record UpdateUserResponse(bool Flag, string Message = null!, ApplicationUser NewUserUpdate = null!);
}
