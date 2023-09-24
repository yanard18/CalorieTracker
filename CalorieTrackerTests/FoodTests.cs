using System.Globalization;

namespace CalorieTracker;

[TestFixture]
public class FoodTests
{
    [Test]
    [TestCase("Json/test.json")]
    [TestCase("Json/apple.json")]
    public void Test_ToString(string jsonPath)
    {
        var food = FoodDeserializer.Read(jsonPath);
        var foodString = food.ToString();

        Assert.That(foodString, Does.Contain(food.Name));
        Assert.That(foodString, Does.Contain(food.Calorie.ToString(CultureInfo.InvariantCulture)));
        Assert.That(foodString, Does.Contain(food.Carb.ToString(CultureInfo.InvariantCulture)));
        Assert.That(foodString, Does.Contain(food.Protein.ToString(CultureInfo.InvariantCulture)));
        Assert.That(foodString, Does.Contain(food.Fat.ToString(CultureInfo.InvariantCulture)));
    }

    [Test]
    public void Test_Equality()
    {
        var food1 = new Food()
        {
            Name = "Test",
            Carb = 200,
            Protein = 30,
            Fat = 15
        };
        var food2 = new Food()
        {
            Name = "Test",
            Carb = 200,
            Protein = 30,
            Fat = 15
        };

        var food3 = new Food()
        {
            Name = "food3",
            Carb = 220,
            Protein = 30,
            Fat = 15
        };
        
        Assert.That(food1, Is.EqualTo(food2));
        Assert.That(food1, Is.Not.EqualTo(food3));
    }

}
