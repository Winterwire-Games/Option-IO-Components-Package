using UnityEngine;

namespace WinterwireGames.OptionIOComponents
{
	[AddComponentMenu("Options/Float Option Writer")]
	public class FloatOptionWriter : OptionWriter<float>
	{
		protected override void WriteOption(string name, float value)
		{
			PlayerPrefs.SetFloat(name, value);
		}
	}
}
