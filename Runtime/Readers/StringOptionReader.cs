using UnityEngine;

namespace WinterwireGames.OptionIOComponents
{
	[AddComponentMenu("Options/String Option Reader")]
	public class StringOptionReader : OptionReader<string>
	{
		protected override string ReadValue(string name, string default_value)
		{
			return PlayerPrefs.GetString(name, default_value);
		}
	}
}
