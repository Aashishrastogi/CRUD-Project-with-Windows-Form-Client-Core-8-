using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Server;

public static class JwtAuthenticationManager
{
   // private static readonly IConfiguration _config;
   private const string JWT_TOKEN_KEY = "1234567890123456789012345678901234567890";
   private const int JWT_TOKEN_VALIDITY = 30;

    public static AuthenticationResponse Authenticate(AuthenticationRequest authenticationRequest)
    {
        if (authenticationRequest.Username != "admin" || authenticationRequest.Password != "password")
        {
            return null;
        }

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var tokenKey = Encoding.ASCII.GetBytes(JWT_TOKEN_KEY);

        var tokenExpiryDateTime = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY);


        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim("Username", authenticationRequest.Username),
                new Claim(ClaimTypes.Role, "Administrator")
            }),

            Expires = tokenExpiryDateTime,

            SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

        var token = jwtSecurityTokenHandler.WriteToken(securityToken);
        return
            new AuthenticationResponse
            {
                AccessToken = token,
                ExpiresIn = (int)tokenExpiryDateTime.Subtract(DateTime.Now).TotalSeconds
            };
    }
}