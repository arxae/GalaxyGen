using System;
using System.Collections.Generic;
using GalaxyGeneratorConsole;

namespace GalaxyGeneratorConsole.Space
{
	public class StarSystem
	{
		public string SystemName;
		public Guid Id;
		public List<Sun> Suns;
		public List<Planet> Planets;

		public StarSystem(int min = 1, int max = 9) : this(false, min, max) { }
		public StarSystem(bool generateSelf, int min = 1, int max = 9)
		{
			Planets = new List<Planet>();
			Suns = new List<Sun>();

			Id = Guid.NewGuid();

			if (generateSelf)
			{
				Generate(min, max);
			}
		}

		public void Generate(int min = 1, int max = 9)
		{
			SystemName = DataLoader.Get().PickUniqueSystemName();

			// Generate Sun(s)
			float extraSunChance = DataLoader.Get().GeneratorVariables.Sun_MultiSunChance;
			while ((100 - DataLoader.Get().Random.NextFloat(100)) < extraSunChance)
			{
				extraSunChance = extraSunChance - DataLoader.Get().GeneratorVariables.Sun_MultiSunDiminishFactor;

				var sun = new Sun();
				sun.Generate();
				Suns.Add(sun);
			}

			// Always make sure a system has at least 1 sun
			if (Suns.Count == 0)
			{
				var primarySun = new Sun();
				primarySun.Generate();
				primarySun.IsPrimary = true;
				primarySun.Name = SystemName;
				Suns.Add(primarySun);
			}


			// Generate Planets
			int planetAmount = DataLoader.Get().Random.Next(min, max);
			for (int i = 0; i < planetAmount; i++)
			{
				var p = new Planet();

				if (i == 0)
				{
					p.Name = SystemName + " Prime";
					p.IsPrime = true;
				}
				else
				{
					p.Name = string.Format("{0} {1}", SystemName, (i + 1).ToRoman());
				}

				p.Generate();

				Planets.Add(p);
			}
		}

		public override string ToString()
		{
			return SystemName;
		}
	}
}
