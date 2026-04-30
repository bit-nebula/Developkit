using BitNebula.DevelopKit.Entities;

namespace BitNebula.DevelopKit.Test.Entities;

[TestClass]
public class Entity_Test
{
    [TestMethod]
    public void TestEntityCreation()
    {
        // Arrange
        long expectedId = 1;
        DateTime expectedDateTime = DateTime.Now;

        // Act
        var entity = new TestEntity(expectedId)
        {
            CreateTime = expectedDateTime,
            UpdateTime = expectedDateTime,
        };

        // Assert
        Assert.AreEqual(expectedId, entity.Id);
        Assert.AreEqual(expectedDateTime, entity.CreateTime);
        Assert.AreEqual(expectedDateTime, entity.UpdateTime);
    }
}

public class TestEntity : Entity
{
    public TestEntity() : base() { }
    public TestEntity(long id) : base(id) { }
}