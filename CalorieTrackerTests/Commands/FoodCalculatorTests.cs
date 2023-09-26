namespace CalorieTracker;

[TestFixture]
public class FoodCalculatorTests
{
    [Test]
    public void Calculate_Returns_Total_Food_Intake()
    {
        var banana = new Food()
        {
            Name = "banana",
            Calorie = 50,
            Carb = 50,
            Fat = 50,
            Protein = 50,
            WeightType = Food.EWeightType.ONE
        };
        var apple = new Food()
        {
            Name = "apple",
            Calorie = 100,
            Carb = 100,
            Fat = 100,
            Protein = 100,
            WeightType = Food.EWeightType.PER100G
        };
        var foods = new List<Food>()
        {
            banana,
            apple
        };
        var currentTotal = FoodCalculator.Calculate(foods);

        const string expected = "Total, Calorie:150, Carb:150, Fat:150, Protein:150";
        Assert.That(currentTotal, Is.EqualTo(expected));
    }


}
