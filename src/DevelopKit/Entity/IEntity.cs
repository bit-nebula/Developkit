namespace BitNebula.DevelopKit.Entity;

public interface IEntity<T> where T : IEquatable<T>
{
    /// <summary>
    /// 主键
    /// </summary>
    T Id { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    DateTime UpdateTime { get; set; }
}
