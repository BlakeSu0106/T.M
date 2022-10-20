namespace Telligent.Member.Domain.Shared.Members;

/// <summary>
/// 帳號啟用狀態
/// </summary>
public enum AccountStatus
{
    /// <summary>
    /// 停用
    /// </summary>
    Deactivated = 0,

    /// <summary>
    /// 正常啟用
    /// </summary>
    Activated = 1,

    /// <summary>
    /// 等待啟用
    /// </summary>
    Pending = 2,

    /// <summary>
    /// 刪除
    /// </summary>
    Deleted = 99,

    /// <summary>
    /// 發生例外
    /// </summary>
    Exception = -1
}