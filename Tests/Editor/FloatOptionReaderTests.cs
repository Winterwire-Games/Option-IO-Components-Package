using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static WinterwireGames.OptionIOComponents.Editor.Tests.PlayerPrefsCacher;

namespace WinterwireGames.OptionIOComponents.Editor.Tests
{
	public class FloatOptionReaderTests
	{
		public static readonly string OPTION_NAME = "Test Option";
		public static PrefSchema CACHE_SCHEMA { get; } = new(new Dictionary<string, PrefType>
		{
			{ OPTION_NAME, PrefType.Float }
		});
		private PrefCache Cache;
		private GameObject GameObject;
		private FloatOptionReader FloatOptionReader;

		[SetUp]
		public void SetUp()
		{
			this.Cache = SavePrefs(CACHE_SCHEMA);
			this.GameObject = new("Test");
			this.FloatOptionReader = this.GameObject.AddComponent<FloatOptionReader>();
			this.FloatOptionReader.OptionName = OPTION_NAME;
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(this.GameObject);
			RestorePrefs(this.Cache);
		}

		[Test]
		public void ReadsFloats(
			[Random(float.MinValue, float.MaxValue, 10)]
			float value)
		{
			PlayerPrefs.SetFloat(OPTION_NAME, value);
			Assert.AreEqual(value, this.FloatOptionReader.Value);
		}

		[Test]
		public void ReadsDefaults(
			[Random(float.MinValue, float.MaxValue, 10)]
			float value)
		{
			PlayerPrefs.DeleteKey(OPTION_NAME);
			this.FloatOptionReader.DefaultValue = value;
			Assert.AreEqual(value, this.FloatOptionReader.Value);
		}
	}
}
