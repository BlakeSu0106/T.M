namespace Telligent.Member.Application.Dtos.Auth
{
    public class ValidateMemberResultDto
    {
        /// <summary>
        /// 會員是否存在
        /// </summary>
        public bool IsExist { get; set; }

        /// <summary>
        /// 會員識別碼
        /// </summary>
        public Guid MemberId { get; set; }
    }
}
