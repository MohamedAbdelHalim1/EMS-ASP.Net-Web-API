using BackendLibrary.Data;
using BackendLibrary.Helpers;
using BackendLibrary.Repositories.Contracts;
using BackendLibrary.DTOs;
using BackendLibrary.Entities;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using BackendLibrary.Responses.UserResponses;


namespace BackendLibrary.Repositories.Implementations.UserImplementations
{
    public class UserAccountRepository(IOptions<JwtSection> config, AppDbContext appDbContext) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAsync(Register user)  //Register -> is an object carry data came from frontend form that you are validate upon it
        {
            if (user is null) return new GeneralResponse(false, "Model is Empty");

            var CheckUser = await FindUserByEmail(user.Email!);
            if (CheckUser != null) return new GeneralResponse(false, "User Registerd Already!");

            //save user 
            var applicationUser = await AddToDataBase(new ApplicationUser()
            {
                Fullname = user.Fullname,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            });

            //create , check , assign role
            //we handle it as first person to register will be automatically assigned as admin and the rest will be a user
            var checkAdminRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.Admin));
            if (checkAdminRole is null)
            {
                var createAdminRole = await AddToDataBase(new SystemRole() { Name = Constants.Admin });
                //AddToDataBase ->After saving changes, the method returns the added entity , and systemRole contain Id and Name , And you added Name = Constant.Admin which equal "Admin" and Id is autoincreament
                await AddToDataBase(new UserRole() { RoleId = createAdminRole.Id, UserId = applicationUser.Id });
                return new GeneralResponse(true, "Account Created!");

            }

            var CheckUserRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.User));
            SystemRole response = new();
            if (CheckUserRole is null)
            {
                response = await AddToDataBase(new SystemRole() { Name = Constants.User });
                await AddToDataBase(new UserRole() { RoleId = response.Id, UserId = applicationUser.Id });
            }
            else
            {

                await AddToDataBase(new UserRole() { RoleId = CheckUserRole.Id, UserId = applicationUser.Id });

            }
            return new GeneralResponse(true, "Account Added!");
        }

        public async Task<LoginResponse> SignInAsync(Login user)
        {
            if (user is null) return new LoginResponse(false, "Model is Empty");
            var applicationUser = await FindUserByEmail(user.Email!);
            if (applicationUser is null) return new LoginResponse(false, "User Not Found , Please Register First");
            if (!BCrypt.Net.BCrypt.Verify(user.Password, applicationUser.Password))
                return new LoginResponse(false, "Wrong Credintials , Try Again");
            var getUserRole = await FindUserRole(applicationUser.Id);
            if (getUserRole is null) return new LoginResponse(false, "Role is not Found");
            var getRoleName = await FindRoleName(getUserRole.RoleId);
            if (getRoleName is null) return new LoginResponse(false, "Role is not found");
            // if there is role and model isnot empty andcredintials is correct , So generate Token now
            string jwtToken = GenerateToken(applicationUser, getRoleName!.Name!);
            string refreshToken = GenerateRefreshToken();
            //save refresh token in database
            var findUser = await appDbContext.RefreshTokens.FirstOrDefaultAsync(_ => _.UserId == applicationUser.Id);
            if (findUser is not null)
            {
                findUser!.Token = refreshToken;
                await appDbContext.SaveChangesAsync();
            }
            else
            {
                await AddToDataBase(new RefreshToken() { Token = refreshToken, UserId = applicationUser.Id });
            }

            return new LoginResponse(true, "Login Successfully", jwtToken, refreshToken);


        }
        private string GenerateToken(ApplicationUser user, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key!));
            var credintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.Fullname!),
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.Role , role!)
            };
            var token = new JwtSecurityToken(
                issuer: config.Value.Issuer,
                audience: config.Value.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credintials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        private async Task<ApplicationUser> FindUserByEmail(string email) =>
            await appDbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Email!.ToLower()!.Equals(email!.ToLower()));

        private async Task<T> AddToDataBase<T>(T model)
        {
            var result = appDbContext.Add(model!);
            await appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }

        private async Task<UserRole> FindUserRole(int userId)
        {
            return await appDbContext.UserRoles.FirstOrDefaultAsync(_ => _.UserId == userId);
        }
        //FindUserRole will return userId and RoleId , RoleId will use in following function to get Role name if admin or user
        private async Task<SystemRole> FindRoleName(int roleId)
        {
            return await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Id == roleId);
        }
        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenInfo refreshToken) //refreshtoken carry what user post which is in DTO , Token , and we will compare with field in database which is also Token
        {
            if (refreshToken is null) return new LoginResponse(false, "Model is Empty");
            var findToken = await appDbContext.RefreshTokens.FirstOrDefaultAsync(_ => _.Token!.Equals(refreshToken.Token));
            if (findToken is null) return new LoginResponse(false, "Token is required");
            //in case it found we need to get user info , we want to GenerateToken again with all argument
            var user = await appDbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Id.Equals(findToken.UserId));
            if (user is null) return new LoginResponse(false, "Token Couldn't be generated Because user not Found");
            var userRole = await FindUserRole(user.Id);
            var roleName = await FindRoleName(userRole.RoleId);
            string jwtToken = GenerateToken(user, roleName.Name!);
            string newRefreshToken = GenerateRefreshToken();
            var updateRefreshToken = await appDbContext.RefreshTokens.FirstOrDefaultAsync(_ => _.UserId.Equals(user.Id));
            if (updateRefreshToken is null) return new LoginResponse(false, "refresh Token could not be generated because user not signed in");
            updateRefreshToken.Token = newRefreshToken;
            await appDbContext.SaveChangesAsync();
            return new LoginResponse(true, "Token refreshed Successfully", jwtToken, newRefreshToken);
        }

        public async Task<ReadUserDataResponse> GetAllUsersAsync()
        {
            var users = await appDbContext.ApplicationUsers.ToListAsync();
            if (users is null) return new ReadUserDataResponse(false, "No Users Found");
            return new ReadUserDataResponse(true, "", users);
        }

        public async Task<ReadUserByIdResponse> GetSpecificUserAsync(int id)
        {
            var user = await appDbContext.ApplicationUsers.Where(_ => _.Id.Equals(id)).FirstOrDefaultAsync();
            if (user is null) return new ReadUserByIdResponse(false, "No User with your Id");
            return new ReadUserByIdResponse(true, "", user);

        }


        public async Task<UpdateUserResponse> UpdateUserAsync(int id, UpdateUser updateUser)
        {
            var user = await appDbContext.ApplicationUsers.Where(_ => _.Id.Equals(id)).FirstOrDefaultAsync();
            user!.Email = updateUser.Email;
            user!.Fullname = updateUser.Fullname;
            await appDbContext.SaveChangesAsync();
            return new UpdateUserResponse(true, "User Updated Successfully", user);
        }

        public async Task<DeleteUserResponse> DeleteUserAsync(int id)
        {
            // Find the user by ID
            var user = await appDbContext.ApplicationUsers.FindAsync(id);

            if (user == null)
            {
                return new DeleteUserResponse(false, "User not found");
            }

            // Remove the user from the DbSet
            appDbContext.ApplicationUsers.Remove(user);

            // Save changes to the database
            await appDbContext.SaveChangesAsync();

            // Return a response indicating success
            return new DeleteUserResponse(true, "User deleted successfully");
        }
    }
}
