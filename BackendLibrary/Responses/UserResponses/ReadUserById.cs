using BackendLibrary.Entities;

namespace BackendLibrary.Responses.UserResponses
{
    public record ReadUserByIdResponse(bool Flag, string Message = null!, ApplicationUser user = null!);
}
