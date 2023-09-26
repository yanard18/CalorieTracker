namespace CalorieTracker;

public static class FoodCalculator
{

    public static string Calculate(List<Food> foods)
    {
        var totalCalorie = 0f;
        var totalCarb = 0f;
        var totalFat = 0f;
        var totalProtein = 0f;
        
        foreach (var food in foods)
        {
            totalCalorie += food.Calorie;
            totalCarb += food.Carb;
            totalFat += food.Fat;
            totalProtein += food.Protein;
        }

        return $"Total, " +
               $"Calorie:{totalCalorie}," +
               $" Carb:{totalCarb}," +
               $" Fat:{totalFat}," +
               $" Protein:{totalProtein}";
    }
}
