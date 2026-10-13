namespace BitNebula.DevelopKit.Platform.Privilege;

/// <summary>
/// 提权结果
/// </summary>
/// <param name="Success"></param>
/// <param name="Error"></param>
/// <param name="Message"></param>
public sealed record ElevationResult(bool Success, ElevationErrorType Error, string? Message = null);
