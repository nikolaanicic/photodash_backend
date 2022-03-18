using Entities.Dtos.Token;
using Entities.Dtos.UserDtos;
using Entities.RequestFeatures;
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
        Task<PagedList<UserForReplyDto>> GetFollowersAsync(ClaimsPrincipal currentPrincipal,FollowersRequestParameters requestQuery);
        Task<IdentityError> Follow(string username, ClaimsPrincipal currentPrincipal);
        Task<IdentityError> Unfollow(string username, ClaimsPrincipal currentPrincipal);

        Task<IEnumerable<UserForReplyDto>> SearchByUsernamePart(string usernameParam, ClaimsPrincipal currentPrincipal);

    }
}
