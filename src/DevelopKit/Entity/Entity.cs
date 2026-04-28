namespace BitNebula.DevelopKit.Entity;

public abstract class Entity : IEntity<long>
{
    public long Id { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
}
