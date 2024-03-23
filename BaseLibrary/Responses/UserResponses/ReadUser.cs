using BaseLibrary.Entities;

namespace BaseLibrary.Responses.UserResponses
{
    public record ReadUserDataResponse(bool Flag, string Message = null!, List<ApplicationUser> Users = null!);

}
