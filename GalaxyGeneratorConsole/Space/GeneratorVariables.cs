using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyGeneratorConsole.Space
{
	public class GeneratorVariables
	{
		public float Sun_MinimalMass;
		public float Sun_MaximumMass;
		public float Sun_MultiSunChance;
		public float Sun_MultiSunDiminishFactor;

		public static GeneratorVariables GetAllVariables()
		{
			var _vars = new GeneratorVariables();
			var config = DataLoader.ParseDataFile("Variables.txt");

			foreach (var entry in config)
			{
				foreach (var property in entry.Value)
				{
					string propertyName = property.Key;
					string propertyValue = property.Value;

					switch (propertyName)
					{
						case "Minimal Mass":
							float minimalMass;
							if (string.IsNullOrWhiteSpace(propertyValue.Replace('.', ',')))
							{
								minimalMass = 0f;
							}
							else
							{
								if (!float.TryParse(propertyValue.Replace('.', ','), out minimalMass))
								{
									throw new Exception(string.Format("Invalid Minimal Mass value in definition {0} (GeneratorVariables.txt)",
										entry.Key));
								}
							}
							_vars.Sun_MinimalMass = minimalMass;
							break;
						case "Maximum Mass":
							float maximumMass;
							if (string.IsNullOrWhiteSpace(propertyValue.Replace('.', ',')))
							{
								maximumMass = 150f;
							}
							else
							{
								if (!float.TryParse(propertyValue.Replace('.', ','), out maximumMass))
								{
									throw new Exception(string.Format("Invalid Maximum Mass value in definition {0} (GeneratorVariables.txt)",
										entry.Key));
								}
							}
							_vars.Sun_MaximumMass = maximumMass;
							break;
						case "Chance For Multiple Suns":
							float multiSunChance;
							if (string.IsNullOrWhiteSpace(propertyValue.Replace('.', ',')))
							{
								multiSunChance = 10f;
							}
							else
							{
								if (!float.TryParse(propertyValue.Replace('.', ','), out multiSunChance))
								{
									throw new Exception(string.Format("Invalid Chance For Multiple Suns value in definition {0} (GeneratorVariables.txt)", entry.Key));
								}
							}
							_vars.Sun_MultiSunChance = multiSunChance;
							break;
						case "Multiple Sun Diminish Factor":
							float multiSunDiminishFactor;
							if (string.IsNullOrWhiteSpace(propertyValue.Replace('.', ',')))
							{
								multiSunDiminishFactor = 5f;
							}
							else
							{
								if (!float.TryParse(propertyValue.Replace('.', ','), out multiSunDiminishFactor))
								{
									throw new Exception(string.Format("Invalid Multiple Sun Diminish Factor value in definition {0} (GeneratorVariables.txt)", entry.Key));
								}
							}
							_vars.Sun_MultiSunDiminishFactor = multiSunDiminishFactor;
							break;
					}
				}
			}

			return _vars;
		}
	}
}
