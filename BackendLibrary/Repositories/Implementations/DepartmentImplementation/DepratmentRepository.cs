using BackendLibrary.Data;
using BackendLibrary.DepartmentValidation;
using BackendLibrary.Entities;
using BackendLibrary.Repositories.Contracts;
using BackendLibrary.Responses.DerpartmentResponse;
using Microsoft.EntityFrameworkCore;

namespace BackendLibrary.Repositories.Implementations.DepartmentImplementation
{
    public class DepratmentRepository(AppDbContext appDbContext) : IDepartment
    {
        public async Task<CreateDepartmentResponseAsync> CreateDepartmentAsync(CreateDepartment createDepartment)
        {
            if (createDepartment is null) return new CreateDepartmentResponseAsync(false, "Model Is Empty");
            var Department = await appDbContext.Departments.FirstOrDefaultAsync(_ => _.Name!.Equals(createDepartment.Name));
            if (Department is not null) return new CreateDepartmentResponseAsync(false, "Department already Exist");
            var NewDepartment = new Department
            {
                Name = createDepartment.Name,
                GeneralDepartmentId = createDepartment.GeneralDepartmentId
            };
            appDbContext.Departments.Add(NewDepartment);
            await appDbContext.SaveChangesAsync();
            return new CreateDepartmentResponseAsync(true, "Department Added Successfully");
            
        }

        public async Task<DepartmentsResponse> DepartmentsAsync()
        {
            var Departments = await appDbContext.Departments.ToListAsync();
            if (Departments is null) return new DepartmentsResponse(false, "No Departments Yet");
            return new DepartmentsResponse(true, "", Departments);

        }
    }
}
