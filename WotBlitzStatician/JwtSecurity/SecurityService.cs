using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.JwtSecurity
{
  public class SecurityService : ISecurityServise
  {
    private readonly ISecurityConfiguration _securityConfiguration;

    public SecurityService(ISecurityConfiguration securityConfiguration)
    {
      _securityConfiguration = securityConfiguration;
    }

    public string Authenticate(string secureWord)
    {
      // string encryptedWord = secureWord.EncryptString(_securityConfiguration.Secret);
      string decryptedsecureWord = secureWord.DecryptString(_securityConfiguration.Secret);

      if(_securityConfiguration.SecureWord != decryptedsecureWord)
      {
        return null;
      }

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityConfiguration.Secret));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      // generate jwt token
      var claims = new[]
       {
            new Claim(ClaimTypes.Name, "adminUser")
      };

      var token = new JwtSecurityToken(
          issuer: "WotBlitzStatician.com",
          audience: "WotBlitzStatician.com",
          claims: claims,
          expires: DateTime.Now.AddMinutes(30),
          signingCredentials: creds);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
