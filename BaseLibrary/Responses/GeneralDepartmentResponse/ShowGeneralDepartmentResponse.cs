using BaseLibrary.Entities;


namespace BaseLibrary.Responses.GeneralDepartmentResponse
{
    public record ShowGeneralDepartmentResponse(bool Flag , string Meassage = null! , List<GeneralDepartment> GeneralDepartments = null!);
}
