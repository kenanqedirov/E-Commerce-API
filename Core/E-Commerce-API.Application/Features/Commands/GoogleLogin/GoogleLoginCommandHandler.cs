using E_Commerce_API.Application.Abstraction.Token;
using E_Commerce_API.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce_API.Application.Features.Commands.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "ygfyugefyugefegksyvdhsch" }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
            var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user is not null;

            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user is null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NameSurname = payload.Name,
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
                await _userManager.AddLoginAsync(user, info);
            else
                throw new Exception("Invalid external authentication");

            return new()
            {
                Token = _tokenHandler.CreateAccessToken(5)
            };
        }
    }
}
