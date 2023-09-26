using System.Globalization;
using System.Text.Json;

namespace CalorieTracker;

[TestFixture]
public class AddCommandTests
{
    readonly AddCommand _addCommand = new AddCommand();

    [SetUp]
    public void Setup()
    {
        SessionManager.Start();
        FoodContainer.Init();
    }

    [TearDown]
    public void Teardown()
    {
        SessionManager.Close();
        FoodContainer.Destroy();
    }

    [Test]
    public void Add_With_Valid_Food_Name_Write_Food_Data_Into_Temp_JSON_File()
    {
        var testFood = FoodDeserializer.Read("Json/test.json");
        var session = SessionManager.Load();
        var savePath = session.SavePath;

        _addCommand.Execute("TestFood", "100");

        var readData = File.ReadAllText(savePath);
        var testFoodAsJson = JsonSerializer.Serialize(testFood);
        Assert.That(readData, Is.EqualTo(testFoodAsJson+"\r\n"));
    }
    [Test]
    public void Add_With_Weight_Type_One_Write_Food_Data()
    {
        var food = FoodDeserializer.Read("Json/apple.json");
        var session = SessionManager.Load();
        var savePath = session.SavePath;

        _addCommand.Execute("apple", "1");

        var expected = JsonSerializer.Serialize(food);
        var readData = File.ReadAllText(savePath);
        Assert.That(readData, Is.EqualTo(expected + "\r\n"));
    }

    [Test]
    [TestCase(100)]
    [TestCase(200)]
    [TestCase(50)]
    public void Add_With_Valid_Food_And_Amount_Argument_Write_Correct_Amount_Of_Properties(float amount)
    {
        var testFood = FoodDeserializer.Read("Json/test.json");
        var session = SessionManager.Load();
        var savePath = session.SavePath;

        var amountString = amount.ToString(CultureInfo.InvariantCulture);
        _addCommand.Execute("TestFood", amountString);

        var resultInSavePath = File.ReadAllText(savePath);

        var calorieAmount = testFood.Calorie / 100f * amount;
        Assert.That(resultInSavePath, Does.Contain(calorieAmount.ToString(CultureInfo.InvariantCulture)));
    }
    [Test]
    public void Add_Append_Food_Data_Into_Temp_JSON_File()
    {
        var session = SessionManager.Load();
        var savePath = session.SavePath;

        _addCommand.Execute("TestFood", "200");
        _addCommand.Execute("TestFood", "100");

        var resultText = File.ReadAllLines(savePath);
        Assert.That(resultText, Has.Length.EqualTo(2));
    }
    [Test]
    public void Add_With_Invalid_Food_Name_Throw_Exception()
    {
        Assert.That(() => _addCommand.Execute("invalid_food", "100"),
            Throws.TypeOf<TerminalArgumentException>());
    }

}
