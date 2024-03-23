using BackendLibrary.Entities;

namespace BackendLibrary.Responses.UserResponses
{
    public record ReadUserDataResponse(bool Flag, string Message = null!, List<ApplicationUser> Users = null!);

}
