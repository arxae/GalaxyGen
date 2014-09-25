using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyGeneratorConsole.Space
{
	public class PlanetSize
	{
		public string Name;
		public int MaxPopulation;
		public bool IsArtificialWorld;

		public static Dictionary<string, PlanetSize> GetAllSizes()
		{
			var _sizes = new Dictionary<string, PlanetSize>();
			var config = DataLoader.ParseDataFile("PlanetSizes.txt");

			foreach (var entry in config)
			{
				if (_sizes.ContainsKey(entry.Key) == false)
				{
					_sizes.Add(entry.Key, new PlanetSize());
				}

				foreach (var property in entry.Value)
				{
					var propertyName = property.Key;
					var propertyValue = property.Value;

					switch (propertyName)
					{
						case "Name": _sizes[entry.Key].Name = propertyValue; break;
						case "Maximum Population":
							int pop;

							if (string.IsNullOrWhiteSpace(propertyValue))
							{
								pop = 0;
							}
							else
							{
								if (!int.TryParse(propertyValue, out pop))
								{
									throw new Exception(string.Format("Invalid Maximum Population value in definition {0} (PlanetSizes.txt)", entry.Key));
								}
							}

							_sizes[entry.Key].MaxPopulation = pop;

							break;
						case "Is Artificial World":
							bool isArtifical;

							if (string.IsNullOrWhiteSpace(propertyValue))
							{
								isArtifical = false;
							}
							else
							{
								if (!bool.TryParse(propertyValue, out isArtifical))
								{
									throw new Exception(string.Format("Invalid Is Artificial World value in definition {0} (PlanetSizes.txt)",
										entry.Key));
								}
							}

							_sizes[entry.Key].IsArtificialWorld = isArtifical;
							break;
					}
				}
			}

			return _sizes;
		}
	}
}
