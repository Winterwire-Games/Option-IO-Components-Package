using UnityEngine;

namespace WinterwireGames.OptionIOComponents
{
	[AddComponentMenu("Options/String Option Writer")]
	public class StringOptionWriter : OptionWriter<string>
	{
		protected override void WriteOption(string name, string value)
		{
			PlayerPrefs.SetString(name, value);
		}
	}
}
