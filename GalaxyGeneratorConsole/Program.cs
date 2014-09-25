using System;
using System.Collections.Generic;
using System.Linq;
using GalaxyGeneratorConsole.Space;

namespace GalaxyGeneratorConsole
{
	class Program
	{
		private static void Main(string[] args)
		{
			DataLoader.Get().Initialize();

			var now = DateTime.Now;

			var galaxy = new List<StarSystem>();

			Console.WriteLine("Generate 10000 starsystems: ");
			for (int i = 0; i < 10000; i++)
			{
				var system = new StarSystem(true);
				galaxy.Add(system);
				Console.WriteLine("Generated starsystem {0} ({1})", i, system.SystemName);

			}

			var duplicateKeys = galaxy.GroupBy(x => x)
										.Where(group => group.Count() > 1)
										.Select(group => group.Key);

			Console.WriteLine("Found {0} duplicates", duplicateKeys.Count());

			var span = DateTime.Now - now;

			Console.WriteLine();
			Console.WriteLine("Done in " + span);
			Console.ReadKey();
		}
	}
}