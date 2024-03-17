using UnityEngine;

namespace WinterwireGames.OptionIOComponents
{
	[AddComponentMenu("Options/Float Option Reader")]
	public class FloatOptionReader : OptionReader<float>
	{
		protected override float ReadValue(string name, float default_value)
		{
			return PlayerPrefs.GetFloat(name, default_value);
		}
	}
}
