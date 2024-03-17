using UnityEngine;

namespace WinterwireGames.OptionIOComponents
{
	[AddComponentMenu("Options/Boolean Option Reader")]
	public class BooleanOptionReader : OptionReader<bool>
	{
		protected override bool ReadValue(string name, bool default_value)
		{
			return PlayerPrefs.GetInt(name, default_value ? 1 : 0) != 0;
		}
	}
}
