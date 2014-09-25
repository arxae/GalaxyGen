using System;

namespace GalaxyGeneratorConsole
{
	public class SpaceGenRandom : Random
	{
		public float NextFloat(float maxValue)
		{
			return NextFloat(0, maxValue);
		}

		public float NextFloat(float minValue, float maxValue)
		{
			return (float)(NextDouble() * (maxValue - minValue) + minValue);
		}
	}
}