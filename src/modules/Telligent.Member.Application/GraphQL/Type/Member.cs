using Telligent.Member.Application.GraphQL.DataLoader;

namespace Telligent.Member.Application.GraphQL.Type
{
    /// <summary>
    /// 會員
    /// </summary>
    public class Member
    {
        /// <summary>
        /// 會員代碼
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 會員姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 電話國碼
        /// </summary>
        public string CountryCallingCode { get; set; }

        /// <summary>
        /// 會員手機
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 會員完整手機
        /// </summary>
        public string FullMobile => CountryCallingCode + Mobile;

        /// <summary>
        /// 帳號
        /// </summary>
        public async Task<Account[]> GetAccountsAsync(AccountsByMemberIdDataLoader dataLoader)
        {
            return await dataLoader.LoadAsync(Id);
        }
    }
}
