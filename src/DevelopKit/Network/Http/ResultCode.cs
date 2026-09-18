namespace BitNebula.DevelopKit.Network.Http;

/// <summary>统一响应码（内置码由 Core 的 <c>DefaultResultCode</c> 直接定义，单一事实源）。</summary>
public readonly partial struct ResultCode : IEquatable<ResultCode>
{
    /// <summary>原始整数码值。</summary>
    public int Value { get; }

    /// <summary>码名称（与静态字段同名）。</summary>
    public string Name { get; }

    /// <summary>是否为成功：定义处声明为成功一类，或等于当前被覆盖后的内置 Success 值（保证覆盖 Success 后仍判成功）。</summary>
    public bool IsSuccess { get; }

    /// <summary>构造（未声明归类）：按码值查全局表补全；未登记的码默认归为失败一类（fail-closed）。</summary>
    public ResultCode(int value, string name) : this(value, name, false) { }

    /// <summary>构造（显式声明归类）：生成代码使用此重载，使归类随码值一起固化。</summary>
    public ResultCode(int value, string name, bool isSuccess)
    {
        Value = value;
        Name = name;
        IsSuccess = isSuccess;
    }

    /// <summary>按整数码值相等。</summary>
    public bool Equals(ResultCode other) => Value == other.Value;
    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is ResultCode other && Value == other.Value;
    /// <inheritdoc />
    public override int GetHashCode() => Value;

    /// <summary>隐式转换为整数码，便于直接赋值给 int 类型的 Code 属性或与裸 int 比较。</summary>
    public static implicit operator int(ResultCode code) => code.Value;
    /// <summary>相等运算符。</summary>
    public static bool operator ==(ResultCode a, ResultCode b) => a.Value == b.Value;
    /// <summary>不等运算符。</summary>
    public static bool operator !=(ResultCode a, ResultCode b) => a.Value != b.Value;

    /// <inheritdoc />
    public override string ToString() => $"{Value} {Name}";
}
