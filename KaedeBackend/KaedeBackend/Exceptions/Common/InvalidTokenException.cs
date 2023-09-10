﻿namespace KaedeBackend.Exceptions.Common
{
    public class InvalidTokenException : BaseException
    {
        public InvalidTokenException()
            : base("errors.com.epicgames.account.account_name_taken", "Sorry, that display name is already taken.", 18006, "")
        {
            StatusCode = 400;
        }
    }
}
