namespace Telligent.Member.Application.GraphQL.Type
{
    /// <summary>
    /// 公司
    /// </summary>
    public class Company
    {
        /// <summary>
        /// 公司代碼
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
