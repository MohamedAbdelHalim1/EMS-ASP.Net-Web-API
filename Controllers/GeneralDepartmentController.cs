using BackendLibrary.Repositories.Contracts;
using BackendLibrary.GeneralDepartmentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralDepartmentController(IGeneralDepartment generalDepartment) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateGeneralDepartment(CreateGeneralDepartment createGeneralDepartment)
        {
            if (createGeneralDepartment is null)
            {
                return BadRequest("Model is Empty");
            }
            var Result = await generalDepartment.CreateGeneralDepartmentAsync(createGeneralDepartment);
            if (Result.Flag)
            {
                return Ok(Result);
            }
            else
            {
                return BadRequest(Result);

            }
        }
        [HttpGet]
        public async Task<IActionResult> ShowGeneralDepartments()
        {
            var results = await generalDepartment.ShowGeneralDepartmentAsync();
            if (results.Flag) return Ok(results);
            return BadRequest(results);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGeneralDepartment(int Id , UpdateGeneralDepartment updateGeneralDepartment)
        {
            var result = await generalDepartment.UpdateGeneralDepartmentAsync(Id, updateGeneralDepartment);
            if (result.Flag)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> ShowSpecificGeneralDepartment(int Id)
        {
            var result = await generalDepartment.ShowSpecificGeneralDepartmentAsync(Id);
            if (result.Flag) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteGeneralDepartment(int Id)
        {
            var result = await generalDepartment.DeleteGeneralDepartmentAsync(Id);
            if (result.Flag) return Ok(result);
            return BadRequest(result);
        }
    }
}
