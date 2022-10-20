using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Member.Domain.Organizations;

[Table("company")]
public class Company : Entity
{
    /// <summary>
    /// 集團識別碼
    /// </summary>
    [Column("group_id")]
    public Guid? GroupId { get; set; }

    /// <summary>
    /// 名稱
    /// </summary>
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [Column("description")]
    public string Description { get; set; }
}