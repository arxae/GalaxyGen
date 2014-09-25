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

			var galaxy = new List<StarSystem>();

			// Generate 100 starsystems
			for (int i = 0; i < 100; i++)
			{
				galaxy.Add(new StarSystem(true));
			}

			Console.WriteLine();
			Console.WriteLine("Done...");
			Console.ReadKey();
		}
	}
}