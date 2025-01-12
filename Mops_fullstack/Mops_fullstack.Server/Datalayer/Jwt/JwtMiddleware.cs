using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Datalayer.Jwt
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _nextRequestDelegate;

        public JwtMiddleware(RequestDelegate nextRequestDelegate)
        {
            _nextRequestDelegate = nextRequestDelegate;
        }

        public async Task Invoke(HttpContext httpContext, IPlayerService playerService, IJwtUtils jwtUtils)
        {
            var token = httpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                await _nextRequestDelegate(httpContext);
                return;
            }

            int? playerId = jwtUtils.ValidatePlayerJwtToken(token);
            if (playerId != null)
            {
                httpContext.Items["Player"] = playerService.GetItem((int)playerId);
                Console.WriteLine($"Found player id {playerId}");
            }

            await _nextRequestDelegate(httpContext);
        }
    }
}
