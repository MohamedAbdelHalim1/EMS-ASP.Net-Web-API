

using BackendLibrary.DepartmentValidation;
using BackendLibrary.Responses.DerpartmentResponse;

namespace BackendLibrary.Repositories.Contracts
{
    public interface IDepartment
    {
        Task<CreateDepartmentResponseAsync> CreateDepartmentAsync(CreateDepartment createDepartment);
        Task<DepartmentsResponse> DepartmentsAsync();

    }
}
