namespace EblanDev.ScenarioCore.UtilsFramework.ID
{
	public static class IDHelper
	{
		public const int Invalid = -1;
		public const int Default = 0;
		
		public static int Combine(int id1, int id2)
		{
			if (id1 == Invalid || id2 == Invalid)
			{
				return Invalid;
			}

			return id1 + (id2 << 16);
		}
		
		public static int Combine(int id1, int id2, int id3)
		{
			if (id1 == Invalid || id2 == Invalid || id3 == Invalid)
			{
				return Invalid;
			}

			return id1 + (id2 << 8) + (id3 << 16);
		}

		public static int GetFirst(int id) => (id << 16) >> 16;
		public static int GetSecond(int id) => id >> 16;

		public static int Next(int id) => id + 1;

		public static bool HasValidID(this IIDContainer idContainer) => idContainer.GetID() != Invalid;
	}
}