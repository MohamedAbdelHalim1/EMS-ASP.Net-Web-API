

using BackendLibrary.Entities;

namespace BackendLibrary.Responses.GeneralDepartmentResponse
{
    public record ShowSpecificGeneralDepartmentResponse(bool Flag , string Message = null! , GeneralDepartment GeneralDepartment = null!);
}
