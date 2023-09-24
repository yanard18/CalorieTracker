namespace CalorieTracker;

[TestFixture]
public class FoodContainerTests
{

    [Test]
    public void Init_Register_All_Json_Files()
    {
        FoodContainer.Init();

        var isFoodExist = FoodContainer.Foods.ContainsKey("TestFood");
        var loadedFoodCount = FoodContainer.Foods.Count;
        Assert.That(isFoodExist, Is.True);
        Assert.That(loadedFoodCount, Is.GreaterThanOrEqualTo(2));
    }

    [TearDown]
    public void Teardown()
    {
        FoodContainer.Destroy();
    }
}
