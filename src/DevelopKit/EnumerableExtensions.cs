namespace BitNebula.DevelopKit;

/// <summary>
/// 提供 IEnumerable 的扩展方法
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// 将序列分批，每批包含指定数量的元素
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="source">源序列</param>
    /// <param name="batchSize">每批元素数量（必须大于 0）</param>
    /// <returns>分批后的序列</returns>
    public static IEnumerable<IReadOnlyList<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(batchSize);

        using var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var batch = new List<T>(batchSize)
            {
                enumerator.Current
            };

            while (batch.Count < batchSize && enumerator.MoveNext())
            {
                batch.Add(enumerator.Current);
            }
            yield return batch;
        }
    }
}
