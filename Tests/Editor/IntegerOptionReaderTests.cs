using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static WinterwireGames.OptionIOComponents.Editor.Tests.PlayerPrefsCacher;

namespace WinterwireGames.OptionIOComponents.Editor.Tests
{
	public class IntegerOptionReaderTests
	{
		public static readonly string OPTION_NAME = "Test Option";
		public static PrefSchema CACHE_SCHEMA { get; } = new(new Dictionary<string, PrefType>
		{
			{ OPTION_NAME, PrefType.Int }
		});
		private PrefCache Cache;
		private GameObject GameObject;
		private IntegerOptionReader IntegerOptionReader;

		[SetUp]
		public void SetUp()
		{
			this.Cache = SavePrefs(CACHE_SCHEMA);
			this.GameObject = new("Test");
			this.IntegerOptionReader = this.GameObject.AddComponent<IntegerOptionReader>();
			this.IntegerOptionReader.OptionName = OPTION_NAME;
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(this.GameObject);
			RestorePrefs(this.Cache);
		}

		[Test]
		public void ReadsIntegers(
			[Random(int.MinValue, int.MaxValue, 10)]
			int value)
		{
			PlayerPrefs.SetInt(OPTION_NAME, value);
			Assert.AreEqual(value, this.IntegerOptionReader.Value);
		}

		[Test]
		public void ReadsDefault(
			[Random(int.MinValue, int.MaxValue, 10)]
			int default_value)
		{
			PlayerPrefs.DeleteKey(OPTION_NAME);
			this.IntegerOptionReader.DefaultValue = default_value;
		}
	}
}
