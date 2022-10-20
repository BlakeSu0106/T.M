namespace Telligent.Member.Application.Configs;

public class Config
{
    public Apis Apis { get; set; }
}

public struct Apis
{
    // ReSharper disable once InconsistentNaming
    public string SmsService { get; set; }
}