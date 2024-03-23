using BackendLibrary.DTOs;
using BackendLibrary.Responses.UserResponses;


namespace BackendLibrary.Repositories.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAsync(Register user); 
        Task<LoginResponse> SignInAsync(Login user);
        Task<LoginResponse> RefreshTokenAsync(RefreshTokenInfo refreshToken);
        Task<ReadUserDataResponse> GetAllUsersAsync(); //this function doesnot take any arguments
        Task<ReadUserByIdResponse> GetSpecificUserAsync(int id);
        Task<UpdateUserResponse> UpdateUserAsync(int id , UpdateUser updateUser);
        Task<DeleteUserResponse> DeleteUserAsync(int id);
    }

    //this interface classes will be implemented in implementation folder
}
