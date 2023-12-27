namespace EblanDev.ScenarioCore.UtilsFramework.Extensions
{
    public static class MathfExtension
    {
        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        public static double Clamp01(double value)
        {
            if (value < 0)
                value = 0;
            else if (value > 1)
                value = 1;
            return value;
        }
        
        public static double Min(double a, double b) => a < b ? a : b;
        
        public static double Max(double a, double b) => a > b ? a : b;

        public static double Lerp(double a, double b, double t) => a + (b - a) * MathfExtension.Clamp01(t);
    }
}