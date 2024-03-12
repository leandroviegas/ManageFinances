using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManageFinances.Helpers
{
    public class JwtSecurityTokenHandlerWrapper
    {
        // Create an instance of JwtSecurityTokenHandler
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        IConfiguration _config;


        public JwtSecurityTokenHandlerWrapper(IConfiguration configuration)
        {
            _config = configuration;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        // Generate a JWT token based on user ID and role
        public string GenerateJwtToken(string userId, string username, string email, string role)
        {
            var key = Encoding.ASCII.GetBytes(_config.GetSection("Jwt:Jwt_secret").Value!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email)
            };

            var identity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Jwt:Jwt_issuer"],
                Audience = _config["Jwt:Jwt_audience"],
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _jwtSecurityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return _jwtSecurityTokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal ValidateJwtToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Jwt_secret"]!);

                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["Jwt:Jwt_issuer"],
                    ValidAudience = _config["Jwt:Jwt_audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                return claimsPrincipal;
        }
    }
}
