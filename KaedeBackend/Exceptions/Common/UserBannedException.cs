namespace KaedeBackend.Exceptions.Common
{
    public class UserBannedException : BaseException
    {
        public UserBannedException()
            : base("errors.com.epicgames.common.authorization.authorization_failed", "Invalid Bearer Token, Please Restart your game if this keeps accouring!",
                  18006, "Auth Failed")
        {
            StatusCode = 404;
        }
    }
}
