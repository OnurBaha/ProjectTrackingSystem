using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails.Types;
using System.Text;

namespace Core.Utilities.Security.Hashing;

public class HashingHelper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512()) 
        {
           
            passwordSalt = hmac.Key; 
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); 
        }

    }
    
    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt, bool isLogin = true)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) 
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i]) 
                {
                    throw new BusinessException(Messages.CoreMessages.UserNotFound);

                }
            }
            return true;
        }
    }
}
