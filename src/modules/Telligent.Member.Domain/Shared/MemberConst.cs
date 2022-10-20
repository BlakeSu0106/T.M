namespace Telligent.Member.Domain.Shared;

public static class MemberConst
{
    public static class Localize
    {
        public static class Accounts
        {
            public const string Existed = "account_existed";
            public const string NotExist = "account_not_exist";
            public const string PasswordEmptyOrNull = "account_password_empty_or_null";
            public const string UserIdNotValidEmail = "account_user_id_not_valid_email";
            public const string CreateAccountFailed = "account_create_failed";
            public const string CaptchaNotEqual = "account_captcha_not_equal";
        }

        public static class Organizations
        {
            public const string IsRoot = "organization_is_root";
        }
    }

    public static class Encryption
    {
        public const string Salt = "E19CBFEF-DA68-446C-AAD5-771D88B018F3";
    }
}