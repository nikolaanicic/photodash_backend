using AutoMapper;
using Contracts.Authentication;
using Contracts.Logger;
using Contracts.RepoManager;
using Contracts.Services.IServices;
using Entities.Dtos.Token;
using Entities.Dtos.UserDtos;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceImplementations
{
    public class UserService : IUserService
    {

        private UserManager<User> _userManager;
        private ILoggerManager _logger;
        private IAuthenticationManager _authManager;
        private IMapper _mapper;
        private IRepositoryManager _repository;


        public UserService(UserManager<User> userManager, ILoggerManager logger, IAuthenticationManager authManager, IMapper mapper,IRepositoryManager repo)
        {
            _repository = repo;
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
            _logger = logger;
        }


        public async Task<TokenReplyDto> AuthenticateUser(UserForAuthenticationDto authUser)
        {
            var user = await _userManager.FindByNameAsync(authUser.UserName);

            if (!await _authManager.ValidateUser(authUser))
            {
                _logger.LogWarn($"Invalid username or password login");
                return null;

            }

            var token = await _authManager.CreateToken();
            return new TokenReplyDto { Token = token };

        }

        public async Task<IEnumerable<IdentityError>> DeleteUser(UserForDeletionDto userForDeletion, ClaimsPrincipal currentPrincipal)
        {
            if (!currentPrincipal.IsInRole("Admiinistrator") && !currentPrincipal.Identity.Name.Equals(userForDeletion.UserName))
            {
                return new List<IdentityError> { new IdentityError { Code = HttpStatusCode.Unauthorized.ToString(), Description = "Unauthorized" } };
            }

            var userEntity = await _userManager.FindByNameAsync(userForDeletion.UserName);

            if(userEntity == null)
            {
                _logger.LogWarn($"User doesn't exist. Username:{userForDeletion.UserName}");
                return new List<IdentityError>{ new IdentityError { Code = HttpStatusCode.NotFound.ToString(),Description="User doesn't exist"}};
            }

            var identityResult = await _userManager.DeleteAsync(userEntity);

            if(!identityResult.Succeeded)
            {
                return identityResult.Errors;
            }

            await _repository.SaveAsync();

            return null;
        }

        public async Task<IdentityError> Follow(string username, ClaimsPrincipal currentPrincipal)
        {

            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return new IdentityError { Code = HttpStatusCode.NotFound.ToString() };
            }
            else if(await _userManager.IsInRoleAsync(user,RolesHolder.Admin) || username.Equals(currentPrincipal.Identity.Name))
            {
                return new IdentityError { Code = HttpStatusCode.BadRequest.ToString() };
            }

            var currentUser = await _userManager.Users.Where(x=>x.UserName.Equals(currentPrincipal.Identity.Name))
                .Include(u=>u.Followers).SingleAsync();

            if(!currentUser.Followers.Contains(user))
            {
                currentUser.Followers.Add(user);
                await _repository.SaveAsync();

            }


            return null;
        }

        public async Task<PagedList<UserForReplyDto>> GetFollowersAsync(ClaimsPrincipal currentPrincipal,FollowersRequestParameters requestQuery)
        {
            var currentUser = await _userManager.Users.Where(x => x.UserName.Equals(currentPrincipal.Identity.Name))
                .Include(u => u.Followers).AsTracking().SingleOrDefaultAsync();

            if (currentUser == null)
                return null;

            var followers = currentUser.Followers
                .Skip((requestQuery.PageNumber - 1) * requestQuery.PageSize)
                .Take(requestQuery.PageSize);
            
            var count = currentUser.Followers.Count;

            var mappedFollowers = _mapper.Map<IEnumerable<UserForReplyDto>>(followers);

            return new PagedList<UserForReplyDto>(mappedFollowers.ToList(), count, requestQuery.PageNumber, requestQuery.PageSize);
        }

        public async Task<UserForReplyDto> GetUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                _logger.LogWarn($"User doesn't exist. Username:{username}");
                return null;
            }

            var mappedUser = _mapper.Map<UserForReplyDto>(user);

            return mappedUser;
        }

        public async Task<IEnumerable<IdentityError>> RegisterUser(UserForCreationDto newUser)
        {

            var userEntity = _mapper.Map<User>(newUser);
            var identityResult = await _userManager.CreateAsync(userEntity,newUser.Password);

            if(!identityResult.Succeeded)
            {
                return identityResult.Errors;
            }

            identityResult = await _userManager.AddToRoleAsync(userEntity, RolesHolder.User);

            if(!identityResult.Succeeded)
            {
                return identityResult.Errors;
            }

            await _repository.SaveAsync();

            return null;
        }

        public async Task<IdentityError> Unfollow(string username, ClaimsPrincipal currentPrincipal)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return new IdentityError { Code = HttpStatusCode.NotFound.ToString() };
            }
            else if (await _userManager.IsInRoleAsync(user, RolesHolder.Admin) || username.Equals(currentPrincipal.Identity.Name))
            {
                return new IdentityError { Code = HttpStatusCode.BadRequest.ToString() };
            }

            var currentUser = await _userManager.Users.Where(x => x.UserName.Equals(currentPrincipal.Identity.Name))
                  .Include(u => u.Followers).AsTracking().SingleOrDefaultAsync();

            if(currentUser.Followers.Contains(user))
            {
                currentUser.Followers.Remove(user);
                await _repository.SaveAsync();

            }


            return null;
        }
    }
}
