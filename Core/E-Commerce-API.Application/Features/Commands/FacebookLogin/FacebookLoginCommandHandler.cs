using E_Commerce_API.Application.Abstraction.Token;
using E_Commerce_API.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Application.Features.Commands.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly HttpClient _httpClient;
        private readonly ITokenHandler _tokenHandler;
        public FacebookLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, IHttpClientFactory httpClientFactroy, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _httpClient = httpClientFactroy.CreateClient();
            _tokenHandler = tokenHandler;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id=5611894844&client_secret=evfevfevfevfky6427&grant_type=client_credentials");
            return new();
        }
    }
}
