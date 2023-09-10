using Jose;
using KaedeBackend.Exceptions;
using KaedeBackend.Exceptions.Common;
using Newtonsoft.Json;
using static KaedeBackend.Utils.Globals;

namespace KaedeBackend.Utils
{
    public class TokenVerify
    {
        public static void Verify(HttpContext context)
        {
            if (IsDebug())
                return;

            if (context.Request.Headers.Authorization.ToString() is null ||
                !context.Request.Headers.Authorization.ToString().Contains("bearer eg1~"))
            {
                throw new InvalidTokenException();
            }

            try
            {
                var token = context.Request.Headers.Authorization.ToString().Replace("bearer eg1~", "");

                var decodedAccess = JsonConvert.DeserializeObject<AccessToken>(JWT.Decode(token));

                var findToken = AccessTokens.FirstOrDefault(x => x.AccessToken == $"eg1~{token}");

                if (findToken is null)
                {
                    throw new InvalidTokenException();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidTokenException();
            }
        }
    }
}
