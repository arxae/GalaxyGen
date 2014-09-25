using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GalaxyGeneratorConsole.Space;

namespace GalaxyGeneratorConsole
{
	public class DataLoader
	{
		#region Singleton

		private static DataLoader _instance;

		public static DataLoader Get()
		{
			return _instance ?? (_instance = new DataLoader());
		}

		#endregion

		public SpaceGenRandom Random;
		public Rant.Engine RantEngine;

		public Dictionary<string, PlanetPhysicalType> PlanetPhysicalTypes;
		public Dictionary<string, AtmosphereType> AtmosphereTypes;
		public Dictionary<string, PlanetSize> PlanetSizes;
		public GeneratorVariables GeneratorVariables;

		private List<string> _usedSystemNames;

		public void Initialize()
		{
			Random = new SpaceGenRandom();

			var vocabs = Rant.Vocabulary.FromMultiDirectory(new[]
			{
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data","Dictionaries","Standard"),
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data","Dictionaries","Galaxy")
			});

			RantEngine = new Rant.Engine(vocabs);

			PlanetPhysicalTypes = PlanetPhysicalType.GetAllTypes();
			AtmosphereTypes = AtmosphereType.GetAllAtmospheres();
			PlanetSizes = PlanetSize.GetAllSizes();
			GeneratorVariables = GeneratorVariables.GetAllVariables();

			_usedSystemNames = new List<string>();
		}

		public string PickUniqueSystemName()
		{
			string systemname = RantEngine.Do("<space-system>");

			if (_usedSystemNames.Contains(systemname))
			{
				systemname = PickUniqueSystemName();
			}

			_usedSystemNames.Add(systemname);

			return systemname;
		}

		public PlanetPhysicalType PickPlanetPhysicalType()
		{
			var keys = PlanetPhysicalTypes.Keys.ToList();
			int index = Random.Next(keys.Count);

			return PlanetPhysicalTypes[keys[index]];
		}

		public AtmosphereType PickAtmosphereType(PlanetPhysicalType planetType)
		{
			var viableAtmospheres = new List<AtmosphereType>();

			foreach (var atmosphereType in AtmosphereTypes)
			{
				if (atmosphereType.Value.AllowedOnPlanetPhysicalTypes.Count == 1)
				{
					if (atmosphereType.Value.AllowedOnPlanetPhysicalTypes[0].Name == "All")
					{
						viableAtmospheres.Add(atmosphereType.Value);
						continue;
					}
				}

				if (atmosphereType.Value.AllowedOnPlanetPhysicalTypes.Contains(planetType))
				{
					viableAtmospheres.Add(AtmosphereTypes[atmosphereType.Key]);
				}
			}

			return viableAtmospheres[Random.Next(viableAtmospheres.Count)];
		}

		public PlanetSize PickPlanetSize(bool naturalOnly)
		{
			var viable = PlanetSizes.Where(size => size.Value.IsArtificialWorld == !naturalOnly).ToList();

			return viable[Random.Next(viable.Count)].Value;
		}

		public static Dictionary<string, Dictionary<string, string>> ParseDataFile(string dataFileName)
		{
			var contents = File.ReadAllLines(Utilities.GetDataFilePath(dataFileName));

			var config = new Dictionary<string, Dictionary<string, string>>();

			bool startStatements = false;
			string currentDefBlock = string.Empty;
			foreach (var _line in contents)
			{
				string line = _line.Trim();

				if (line.StartsWith("*END*")) break; // end of the file
				if (line.StartsWith("=")) continue; // comment lines
				if (line.StartsWith("//")) continue; // comment lines
				if (string.IsNullOrWhiteSpace(line)) continue;

				if (line.StartsWith("*BEGIN*"))
				{
					startStatements = true;
					continue;
				}

				if (startStatements == false) continue;

				if (line.StartsWith("Def") && currentDefBlock == string.Empty)
				{
					string defName = line.Split(new[] { ' ' }, 2)[1].Trim();

					if (config.ContainsKey(defName) == false)
					{
						config.Add(defName, new Dictionary<string, string>());
						currentDefBlock = defName;
					}
					else
					{
						throw new Exception(string.Format("Found a duplicate definition of {0} (filenamehere)", defName));
					}

					continue;
				}

				if (line.StartsWith("EndDef"))
				{
					currentDefBlock = string.Empty;
					continue;
				}

				var parts = line.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

				//if (parts.Length != 2)
				//{
				//	throw new Exception(string.Format("Malformed property: {0} ({1})", line, dataFileName));
				//}

				string property = parts[0].Trim();
				string value = string.Empty;

				if (parts.Length > 1)
				{
					value = parts[1].Split(new[] { "//" }, StringSplitOptions.None)[0].Trim();
				}

				//var property = parts[0].Trim();
				//var value = parts[1].Split(new[] { "//" }, StringSplitOptions.None)[0].Trim();

				config[currentDefBlock].Add(property, value);
			}

			return config;
		}
	}
}