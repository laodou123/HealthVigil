using System.Diagnostics.CodeAnalysis;

namespace HealthcareMonitoring.Shared.Services;

public interface IAppContext
{
    /// <summary>
    /// 获得/设置 当前登录账号
    /// </summary>
    [NotNull]
    string? UserName { get; set; }

    /// <summary>
    /// 获得/设置 当前用户显示名称
    /// </summary>
    [NotNull]
    string? DisplayName { get; set; }

    /// <summary>
    /// 是否是医生
    /// </summary>
    bool IsDoctor { get; set; }
}
