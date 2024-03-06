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
        /*****          This is the starting step of the JWT Token generation      *****/
        
        #region VerifyUserAndAssigningRole
        
        
        
        //fetching data which came in and along the request from the client
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
            return null!;
        }

        #endregion
        
        #region SecurityTokenDescriptor

        // reading the raw key from the appsetting.json .the same will be used for authentication by the services
        var tokenKey = Encoding.ASCII.GetBytes(_config.GetValue<string>("JWT_TOKEN_KEY") ??
                                               throw new Exception("Unable to read key from app.config"));

        //  defines and sets the token expiry time of the token
        var tokenExpiryDateTime = DateTime.Now.AddMinutes(_config.GetValue<Int32>("JWT_TOKEN_VALIDITY"));

        // this class is the main class for setting the value and properties of the token . this is the most
        // important step of the token generation as it defines the number and type of claims , encryption algorithm
        // and much more ....
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

        #endregion

        #region JwtSecurityTokenHandler

        // At this Step we are fabricating the token and write the token with the credentials (payload) .
        // The class naming JwtSecurityTokenHandler is used perform operation like these 
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        
        //token creating ...created token as of this point all the info declared i.e claims and signing key will
        //be used to manufacture a token,
        //that is why securityTokenDescriptor is used as a input parameter to fetch the schematics and data .
        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        
        //after the token is manufactured now we will serialize the token which is an necessary step to send it over.
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);
        
        //var token is the final token generated......

       
        #endregion

        /*****          This Marks the complete generation of the JWT token read to be used         *****/
        return
            new AuthenticationResponse
            {
                AccessToken = token,
                ExpiresIn = (int)tokenExpiryDateTime.Subtract(DateTime.Now).TotalSeconds
            };
    }
}