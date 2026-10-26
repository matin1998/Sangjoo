using Sangjoo.Application.DTOs.Account;
using Sangjoo.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Interfaces;

public interface IAccountService
{
    /*Task<IdentityResult> RegisterAsync(RegisterDTO model);*/
    Task<SignInResult> LoginAsync(LoginDTO model);
    Task LogoutAsync();
    Task<ApplicationUser?> GetUserByEmailAsync(string email);

    Task<ApplicationUser?> GetUserByIdAsync(long id);

    Task<IList<string>> GetRolesAsync(ApplicationUser user);

    Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);

    Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string role);

    Task<EditProfileDTO> GetProfileAsync(long userId);

    Task<IdentityResult> UpdateProfileAsync(long userId,EditProfileDTO model);

    Task<IdentityResult> ChangePasswordAsync(
    long userId,
    ChangePasswordDTO model);
}
