using System.Diagnostics.CodeAnalysis;

namespace HealthcareMonitoring.Shared.Services;

/// <summary>
/// IAppContext 实现类
/// </summary>
public class DefaultAppContext : IAppContext
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [NotNull]
    public string? UserName { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [NotNull]
    public string? DisplayName { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsDoctor { get; set; }
}
