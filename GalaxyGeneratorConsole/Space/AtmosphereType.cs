using System;
using System.Collections.Generic;

namespace GalaxyGeneratorConsole.Space
{
	public class AtmosphereType
	{
		public string Name;
		public string Description;
		public List<PlanetPhysicalType> AllowedOnPlanetPhysicalTypes;
		public List<PlanetSize> AllowedOnPlanetSize;

		public AtmosphereType()
		{
			AllowedOnPlanetPhysicalTypes = new List<PlanetPhysicalType>();
		}

		public static Dictionary<string, AtmosphereType> GetAllAtmospheres()
		{
			var _types = new Dictionary<string, AtmosphereType>();
			var config = DataLoader.ParseDataFile("AtmosphereTypes.txt");

			foreach (var entry in config)
			{
				if (_types.ContainsKey(entry.Key) == false)
				{
					_types.Add(entry.Key, new AtmosphereType());
				}

				foreach (var property in entry.Value)
				{
					var propertyName = property.Key;
					var propertyValue = property.Value;

					switch (propertyName)
					{
						case "Name": _types[entry.Key].Name = propertyValue; break;
						case "Description": _types[entry.Key].Description = propertyValue; break;
						case "Allowed On Planet Physical Types":
							var planetPhysicalTypesValues = propertyValue.Split(',');

							if (planetPhysicalTypesValues.Length == 1) // "All" entry
							{
								if (planetPhysicalTypesValues[0].Trim() == "All")
								{
									_types[entry.Key].AllowedOnPlanetPhysicalTypes.Add(PlanetPhysicalType.GetAllType());
								}
								else
								{
									throw new Exception(string.Format("Expected All in {0} (AtmosphereType.txt)", propertyName));
								}
							}
							else
							{
								foreach (var value in planetPhysicalTypesValues)
								{
									var planetType = DataLoader.Get().PlanetPhysicalTypes[value.Trim()];
									_types[entry.Key].AllowedOnPlanetPhysicalTypes.Add(planetType);
								}
							}

							break;
						default:
							throw new Exception(string.Format("Error with the \"{0}\" key in {1} (AtmosphereType.txt)",
								propertyName,
								entry.Key
								));
					}
				}
			}

			return _types;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}