using System;
using System.Collections.Generic;

namespace GalaxyGeneratorConsole.Space
{
	public class PlanetPhysicalType
	{
		public string Name;
		public string Description;
		public int MinimalDistanceFromTheSun;

		public static Dictionary<string, PlanetPhysicalType> GetAllTypes()
		{
			var _types = new Dictionary<string, PlanetPhysicalType>();
			var config = DataLoader.ParseDataFile("PlanetPhysicalTypes.txt");

			foreach (var entry in config)
			{
				if (_types.ContainsKey(entry.Key) == false)
				{
					_types.Add(entry.Key, new PlanetPhysicalType());
				}

				foreach (var property in entry.Value)
				{
					var propertyName = property.Key;
					var propertyValue = property.Value;

					switch (propertyName)
					{
						case "Name": _types[entry.Key].Name = propertyValue; break;
						case "Description": _types[entry.Key].Description = propertyValue; break;
						case "Minimal Distance From The Sun":
							int minimalDistanceFromTheSun;
							if (string.IsNullOrWhiteSpace(propertyValue))
							{
								minimalDistanceFromTheSun = 0;
							}
							else
							{
								if (!int.TryParse(propertyValue, out minimalDistanceFromTheSun))
								{
									throw new Exception(string.Format("Invalid Minimal Distance From The Sun value in definition {0} (PlanetPhysicalTypes.txt)", entry.Key));
								}
							}

							_types[entry.Key].MinimalDistanceFromTheSun = minimalDistanceFromTheSun;
							break;
						default:
							throw new Exception(string.Format("Error with the \"{0}\" key in {1} (PlanetPhysicalTypes.txt)",
								propertyName,
								entry.Key
								));
					}
				}
			}

			return _types;
		}

		/// <summary>
		/// Returns the speciall "All" planet type
		/// </summary>
		/// <returns></returns>
		public static PlanetPhysicalType GetAllType()
		{
			return new PlanetPhysicalType()
			{
				Name = "All",
				Description = "Available on all planet physical types"
			};
		}

		public override string ToString()
		{
			return Name;
		}
	}
}