using System;

namespace GalaxyGeneratorConsole
{
	public class Utilities
	{
		public static string GetDataFilePath(string dataFile)
		{
			return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", dataFile);
		}
	}
}