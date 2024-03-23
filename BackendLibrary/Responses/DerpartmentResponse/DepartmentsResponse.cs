

using BackendLibrary.Entities;

namespace BackendLibrary.Responses.DerpartmentResponse
{
    public record DepartmentsResponse(bool Flag, string Message = null! , List<Department> Departments = null!);
}
