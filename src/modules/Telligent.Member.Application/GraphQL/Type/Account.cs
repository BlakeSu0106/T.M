namespace Telligent.Member.Application.GraphQL.Type
{
    /// <summary>
    /// 帳號
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 帳號代碼
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 會員代碼
        /// </summary>
        public Guid MemberId { get; set; }

        /// <summary>
        /// 帳號來源類別
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// 登入帳號
        /// </summary>
        public string UserId { get; set; }
    }
}
