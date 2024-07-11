using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core.Utilities.Security.Encryption;

public class SecurityKeyHelper
{
    public static SecurityKey CreateSecurityKey(string securityKey)
    {
        //appsettings.json da bulunan "mysupersecretkeymysupersecretkey"
        //stringi byte array haline ve simetrik security key haline getirir.
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
}