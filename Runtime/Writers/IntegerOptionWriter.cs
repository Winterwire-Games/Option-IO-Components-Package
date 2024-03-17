using UnityEngine;

namespace WinterwireGames.OptionIOComponents
{
	[AddComponentMenu("Options/Integer Option Writer")]
	public class IntegerOptionWriter : OptionWriter<int>
	{
		protected override void WriteOption(string name, int value)
		{
			PlayerPrefs.SetInt(name, value);
		}
	}
}
