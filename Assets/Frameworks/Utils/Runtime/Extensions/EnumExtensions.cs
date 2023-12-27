using System;
using System.Collections.Generic;
using System.Linq;

namespace EblanDev.ScenarioCore.UtilsFramework.Extensions
{
    public static class EnumExtensions
    {
        public static bool ContainsFlagsSet<TFlagsEnum>(this TFlagsEnum input, TFlagsEnum other) where TFlagsEnum : Enum
        {
            CheckIsEnum<TFlagsEnum>(true);
            return other.GetFlags().All(input.HasFlag);
        }

        private static IEnumerable<Enum> GetFlags(this Enum input)
        {
            return Enum
                .GetValues(input.GetType())
                .Cast<Enum>()
                .Where(v => Equals((int) (object) v, 0) == false && input.HasFlag(v));
        }

        private static void CheckIsEnum<T>(bool withFlags)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"Type '{typeof(T).FullName}' is not an enum");
            if (withFlags && !Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
                throw new ArgumentException($"Type '{typeof(T).FullName}' doesn't have the 'Flags' attribute");
        }
    }
}