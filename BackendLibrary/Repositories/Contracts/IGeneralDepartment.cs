

using BackendLibrary.GeneralDepartmentValidation;
using BackendLibrary.Responses.GeneralDepartmentResponse;

namespace BackendLibrary.Repositories.Contracts
{
    public interface IGeneralDepartment
    {
        Task<CreateGeneralDepartmentResponse> CreateGeneralDepartmentAsync(CreateGeneralDepartment createGeneralDepartment);
        Task<ShowGeneralDepartmentResponse> ShowGeneralDepartmentAsync();
        Task<ShowSpecificGeneralDepartmentResponse> ShowSpecificGeneralDepartmentAsync(int Id);
        Task<UpdateGeneralDepartmentResponse> UpdateGeneralDepartmentAsync(int Id , UpdateGeneralDepartment updateGeneralDepartment);
        Task<DeleteGeneralDepartmentResponse> DeleteGeneralDepartmentAsync(int Id);

    }
}
