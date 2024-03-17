using UnityEngine;

namespace WinterwireGames.OptionIOComponents
{
	[AddComponentMenu("Options/Integer Option Reader")]
	public class IntegerOptionReader : OptionReader<int>
	{
		protected override int ReadValue(string name, int default_value)
		{
			return PlayerPrefs.GetInt(name, default_value);
		}
	}
}
