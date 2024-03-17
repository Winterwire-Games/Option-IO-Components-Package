using UnityEngine;

namespace WinterwireGames.OptionIOComponents
{
	[AddComponentMenu("Options/Boolean Option Writer")]
	public class BooleanOptionWriter : OptionWriter<bool>
	{
		protected override void WriteOption(string name, bool value)
		{
			PlayerPrefs.SetInt(name, value ? 1 : 0);
		}
	}
}
