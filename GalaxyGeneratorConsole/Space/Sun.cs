namespace GalaxyGeneratorConsole.Space
{
	public class Sun : SpaceSystemObject
	{
		public float Mass;
		public bool IsPrimary;

		public override void Generate()
		{
			float minMass = DataLoader.Get().GeneratorVariables.Sun_MinimalMass;
			float maxMass = DataLoader.Get().GeneratorVariables.Sun_MaximumMass;

			Mass = DataLoader.Get().Random.NextFloat(minMass, maxMass);
		}
	}
}
