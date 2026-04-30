namespace BitNebula.DevelopKit.Entities;

public abstract class Entity : IEntity<long>
{
    protected Entity() { }

    protected Entity(long id)
    {
        Id = id;
    }

    public long Id { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
}
