namespace FitTrack.Helpers;

public static class BmiCalculator
{
    public static double CalculateBmi(int heightCm, double weightKg)
    {
        double heightM = heightCm / 100.0;

        return Math.Round(weightKg / (heightM * heightM), 1);
    }

    public static string GetCategory(double bmi)
    {
        if (bmi < 18.5)
        {
            return "Underweight";
        }

        if (bmi < 25)
        {
            return "Normal";
        }

        if (bmi < 30)
        {
            return "Overweight";
        }

        return "Obese";
    }
}
