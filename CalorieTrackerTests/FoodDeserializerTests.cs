namespace CalorieTracker;

[TestFixture]
public class FoodDeserializerTests
{
    readonly Food _testFood = new Food()
    {
        Name = "TestFood",
        Calorie = 500,
        Carb = 100,
        Fat = 20,
        Protein = 25
    };

    [Test]
    public void Read_Food_Data_From_Json()
    {
        const string jsonPath = @"Json/test.json";

        var food = FoodDeserializer.Read(jsonPath);

        Assert.That(_testFood, Is.EqualTo(food));
    }

    [Test]
    public void Read_With_Invalid_Json_Throw_FileNotFound_Exception()
    {
        const string jsonPath = "Json/invalid.json";

        Assert.That(() => FoodDeserializer.Read(jsonPath),
            Throws.TypeOf<FileNotFoundException>());
    }

    [Test]
    public void Test_Is_Valid()
    {
        const string invalidFoodPath = "Json/session.json";
        const string validFoodPath = "Json/test.json";

        var validFoodPathResult = FoodDeserializer.IsValid(validFoodPath);
        var invalidFoodPathResult = FoodDeserializer.IsValid(invalidFoodPath);
        Assert.That(validFoodPathResult, Is.True);
        Assert.That(invalidFoodPathResult, Is.Not.True);

    }

}
