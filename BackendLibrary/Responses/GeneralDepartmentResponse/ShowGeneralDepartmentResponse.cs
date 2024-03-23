using BackendLibrary.Entities;


namespace BackendLibrary.Responses.GeneralDepartmentResponse
{
    public record ShowGeneralDepartmentResponse(bool Flag , string Meassage = null! , List<GeneralDepartment> GeneralDepartments = null!);
}
