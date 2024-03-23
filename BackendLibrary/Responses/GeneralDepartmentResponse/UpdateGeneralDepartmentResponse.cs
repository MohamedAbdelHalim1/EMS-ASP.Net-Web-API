

using BackendLibrary.Entities;

namespace BackendLibrary.Responses.GeneralDepartmentResponse
{
   public record UpdateGeneralDepartmentResponse(bool Flag , string Message = null! , GeneralDepartment NewGeneralDepartment = null!);
}
