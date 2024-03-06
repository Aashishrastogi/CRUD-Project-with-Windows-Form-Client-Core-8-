using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Server;

public class JwtAuthenticationManager
{
    public JwtAuthenticationManager(IConfiguration configuration)
    {
        _config = configuration;
    }

    private static string _userRole = string.Empty;
    private readonly IConfiguration _config;

    public AuthenticationResponse Authenticate(AuthenticationRequest authenticationRequest)
    {
        if (authenticationRequest.Username == "admin" || authenticationRequest.Password == "admin")
        {
            _userRole = "Admin";
        }
        else if (authenticationRequest.Username == "User" || authenticationRequest.Password == "User")
        {
            _userRole = "User";
        }
        else
        {
            return null;
        }

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var tokenKey = Encoding.ASCII.GetBytes(_config.GetValue<string>("JWT_TOKEN_KEY") ??
                                               throw new Exception("Unable to read key from app.config"));

        var tokenExpiryDateTime = DateTime.Now.AddMinutes(_config.GetValue<Int32>("JWT_TOKEN_VALIDITY"));

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim("Username", authenticationRequest.Username),
                new Claim(ClaimTypes.Role, _userRole)
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