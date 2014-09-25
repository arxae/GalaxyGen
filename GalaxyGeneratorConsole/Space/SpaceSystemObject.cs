using System;

namespace GalaxyGeneratorConsole.Space
{
	public class SpaceSystemObject
	{
		public string Name;
		public int OrbitDistance;
		public SpaceSystemObject OrbitParent;

		public virtual void Generate()
		{
			
		}
	}
}
