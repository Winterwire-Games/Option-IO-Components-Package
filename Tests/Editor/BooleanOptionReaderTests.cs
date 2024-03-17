using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static WinterwireGames.OptionIOComponents.Editor.Tests.PlayerPrefsCacher;

namespace WinterwireGames.OptionIOComponents.Editor.Tests
{
	public class BooleanOptionReaderTests
	{
		public static readonly string OPTION_NAME = "Test Option";
		public static PrefSchema CACHE_SCHEMA { get; } = new(new Dictionary<string, PrefType>
		{
			{ OPTION_NAME, PrefType.Int }
		});
		private PrefCache Cache;
		private GameObject GameObject;
		private BooleanOptionReader BooleanOptionReader;

		[SetUp]
		public void Setup()
		{
			this.Cache = SavePrefs(CACHE_SCHEMA);
			this.GameObject = new("Test");
			this.BooleanOptionReader = this.GameObject.AddComponent<BooleanOptionReader>();
			this.BooleanOptionReader.OptionName = OPTION_NAME;
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(this.GameObject);
			RestorePrefs(this.Cache);
		}

		[TestCase(1, true)]
		[TestCase(0, false)]
		public void ReadsBooleans(int written_value, bool expected_value)
		{
			PlayerPrefs.SetInt(OPTION_NAME, written_value);
			Assert.AreEqual(expected_value, this.BooleanOptionReader.Value);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void ReadsDefaults(bool default_value)
		{
			PlayerPrefs.DeleteKey(OPTION_NAME);
			this.BooleanOptionReader.DefaultValue = default_value;
			Assert.AreEqual(default_value, this.BooleanOptionReader.Value);
		}
	}
}
