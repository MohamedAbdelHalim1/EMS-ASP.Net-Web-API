using BackendLibrary.Data;
using BackendLibrary.Repositories.Contracts;
using BackendLibrary.Entities;
using BackendLibrary.GeneralDepartmentValidation;
using BackendLibrary.Responses.GeneralDepartmentResponse;
using Microsoft.EntityFrameworkCore;

namespace BackendLibrary.Repositories.Implementations.GeneralDepartmentImplementaions
{
    public class GeneralDepartmentRepository(AppDbContext appDbContext) : IGeneralDepartment
    {
        public async Task<CreateGeneralDepartmentResponse> CreateGeneralDepartmentAsync(CreateGeneralDepartment createGeneralDepartment)
        {
            if (createGeneralDepartment is null) return new CreateGeneralDepartmentResponse(false, "Model is empty");

            var CheckGeneralDepartment = await appDbContext.GeneralDepartments.FirstOrDefaultAsync(_ => _.Name!.Equals(createGeneralDepartment.Name));
            if (CheckGeneralDepartment is not null) return new CreateGeneralDepartmentResponse(false, "This GD is already Added");

            if (createGeneralDepartment.Name != null)
            {
                var newGeneralDepartment = new GeneralDepartment
                {
                    Name = createGeneralDepartment.Name
                };
                // Add the new GeneralDepartmentEntity to the DbContext
                appDbContext.GeneralDepartments.Add(newGeneralDepartment);

                // Save changes to the database
                await appDbContext.SaveChangesAsync();

                // Return the response with the created entity details
                return new CreateGeneralDepartmentResponse(true, "General Department created successfully");


            }

            // Default return if none of the conditions are met
            return new CreateGeneralDepartmentResponse(false, "Unexpected error occurred");

        }

        public async Task<DeleteGeneralDepartmentResponse> DeleteGeneralDepartmentAsync(int Id)
        {
            var GeneralDepartment = await appDbContext.GeneralDepartments.FirstOrDefaultAsync(_ => _.Id.Equals(Id));
            if (GeneralDepartment is null) return new DeleteGeneralDepartmentResponse(false, "No General Department with this Id");
            appDbContext.GeneralDepartments.Remove(GeneralDepartment);
            await appDbContext.SaveChangesAsync();
            return new DeleteGeneralDepartmentResponse(true, "General Department Deleted Successfully");
        }

        public async Task<ShowGeneralDepartmentResponse> ShowGeneralDepartmentAsync()
        {
            var GeneralDepartments = await appDbContext.GeneralDepartments.ToListAsync();
            if (GeneralDepartments is null) return new ShowGeneralDepartmentResponse(false, "No General Departments Yet.");
            return new ShowGeneralDepartmentResponse(true, "", GeneralDepartments);
        }

        public async Task<ShowSpecificGeneralDepartmentResponse> ShowSpecificGeneralDepartmentAsync(int Id)
        {
            var GeneralDepartment = await appDbContext.GeneralDepartments.FirstOrDefaultAsync(_ => _.Id.Equals(Id));
            if (GeneralDepartment is null) return new ShowSpecificGeneralDepartmentResponse(false, "No General Department with this Id");
            return new ShowSpecificGeneralDepartmentResponse(true, "", GeneralDepartment);
        }

        public async Task<UpdateGeneralDepartmentResponse> UpdateGeneralDepartmentAsync(int Id , UpdateGeneralDepartment updateGeneralDepartment)
        {
            if (updateGeneralDepartment is null) return new UpdateGeneralDepartmentResponse(false, "Model is Empty!");
            var NewGeneralDepartment = await appDbContext.GeneralDepartments.FirstOrDefaultAsync(_ => _.Id.Equals(Id));
            if (NewGeneralDepartment is null) return new UpdateGeneralDepartmentResponse(false, "No General Department match Id");
            NewGeneralDepartment!.Name = updateGeneralDepartment.Name;
            await appDbContext.SaveChangesAsync();
            return new UpdateGeneralDepartmentResponse(true, "General Department Updated Successfully", NewGeneralDepartment);
        }
    }
}
