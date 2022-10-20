using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Application.Dtos.Auth
{
    public class ValidateAccountResultDto
    {
        /// <summary>
        /// 帳號是否存在
        /// </summary>
        public bool IsExist { get; set; }

        /// <summary>
        /// 密碼是否為空
        /// </summary>
        public bool IsPasswordEmpty { get; set; }

        /// <summary>
        /// 帳號狀態
        /// </summary>
        public AccountStatus AccountStatus { get; set; }

    }
}
