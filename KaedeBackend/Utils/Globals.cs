using KaedeBackend.Models.Auth;
using KaedeBackend.Models.Other;
using System.Security.Cryptography;
using System.Text;

namespace KaedeBackend.Utils
{
    public static class Globals
    {
        public static string FromBytes(this byte[] bytes) => Encoding.UTF8.GetString(bytes);
        public static byte[] ToBytes(this string txt) => Encoding.UTF8.GetBytes(txt);

        public static string DecodeBase64(this string txt) => Convert.FromBase64String(txt).FromBytes();

        public static string CreateUuid() => Guid.NewGuid().ToString().Replace("-", string.Empty);

        public static string CurrentTime() => DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");

        public static string ComputeSHA256Hash(this string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

        public static readonly string JWT_SECRET = CreateUuid();
        public static List<AccessTokensGlobal> AccessTokens = new List<AccessTokensGlobal>();
        public static List<AccessTokensGlobal> RefreshTokens = new List<AccessTokensGlobal>();

        public static SeasonData GetSeasonData(HttpContext context)
        {
            var seasonData = new SeasonData()
            {
                Build = 0,
                Lobby = "",
                Season = 0,
            };

            double i = 8.51;

            var userAgent = context.Request.Headers["User-Agent"].ToString();

            if (userAgent != null)
            {
                try
                {
                    var build = userAgent.Split("Release-")[1].Split("-")[0];

                    var value = build.Split(".");


                    seasonData.Build = double.Parse(build);
                    seasonData.Season = int.Parse(build.Split(".")[0]);
                    seasonData.Lobby = $"LobbySeason{seasonData.Season}";
                }
                catch
                {
                    return seasonData;
                }
            }

            return seasonData;
        }

        public static bool IsDebug()
        {
#if DEBUG 
            return true;
#endif
            return false;
        }
    }
}
