namespace GalaxyGeneratorConsole.Space
{
	public class Planet : SpaceSystemObject
	{
		// SystemName is set by the system generating the planet (system keeps count)
		public bool IsPrime; // Prime planets get some extra stuff later on
		public PlanetPhysicalType PhysicalType;
		public AtmosphereType Atmosphere;
		public PlanetSize Size;

		public override void Generate()
		{
			// TODO: generate moons
			// extra variable for a moon spawning chance in variables.txt
			PhysicalType = DataLoader.Get().PickPlanetPhysicalType();
			Size = DataLoader.Get().PickPlanetSize(true);
			Atmosphere = DataLoader.Get().PickAtmosphereType(PhysicalType);
		}
	}
}