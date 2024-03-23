using BaseLibrary.Entities;

namespace BaseLibrary.Responses.UserResponses
{
    public record ReadUserByIdResponse(bool Flag, string Message = null!, ApplicationUser user = null!);
}
