using Entities.Dtos.Token;
using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Contracts.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<IdentityError>> RegisterUser(UserForCreationDto newUser);
        Task<TokenReplyDto> AuthenticateUser(UserForAuthenticationDto authUser);
        Task<IEnumerable<IdentityError>> DeleteUser(UserForDeletionDto userForDeletion,ClaimsPrincipal currentPrincipal);
        Task<UserForReplyDto> GetUser(string username);

    }
}
