using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Member.Domain.Campaign;

/// <summary>
/// 批量匯入明細檔
/// </summary>
[Table("CPN_FileDetail")]
public class FileDetail : BaseEntity
{
    [Key] [Column("seqNo")] public int SeqNo { get; set; }
    [Column("fileNo")] public int FileNo { get; set; }
    [Column("order")] public int Order { get; set; }
    [Column("accountId")] public string AccountId { get; set; }
    [Column("accountType")] public string AccountType { get; set; }
    [Column("memberId")] public string MemberId { get; set; }
    [Column("prospectiveCustomerNo")] public int ProspectiveCustomerNo { get; set; }
}